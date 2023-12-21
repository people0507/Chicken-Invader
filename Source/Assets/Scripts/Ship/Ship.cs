using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ship : MonoBehaviour
{
    private Rigidbody2D myBody;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shield;


    //fire
    [SerializeField] private GameObject[] bulletList;
    [SerializeField] private int currenTierBullet;
    [SerializeField, Range(0, 1)] private float timeFire;

    private float nextTimeFire = 0f;

    private AudioManager audioManager;
    private int health;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        this.health = 3;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    void FixedUpdate()
    {
        ShipMovement();

    }
    void ShipMovement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeFire)
        {
            Instantiate(bulletList[currenTierBullet], transform.position, Quaternion.identity);
            nextTimeFire = Time.time + timeFire;
            audioManager.PlayFire(audioManager.fireClip);
        }

    }

    private void OnTriggerExit2D(Collider2D target)
    {

        if(target.tag == "Present" && currenTierBullet < 5)
        {
            audioManager.PlayLevelUp(audioManager.levelUpAudioClip);
            this.currenTierBullet += 1;
            this.timeFire -= 0.01f;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( shield == null && (collision.gameObject.tag == "RedChicken" || collision.gameObject.tag == "Egg"))
        {
            if (health > 0)
            {
                this.health--;
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
            }
            else
            {
                Destroy(gameObject);
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
                audioManager.PlayGameOver(audioManager.gameOverClip);
                Time.timeScale = 0.1f;
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
}
