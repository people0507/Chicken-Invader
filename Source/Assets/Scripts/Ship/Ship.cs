using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class Ship : MonoBehaviour
{
    private Rigidbody2D myBody;

    [SerializeField] private GameObject explosion;
    private Shield shield;

    //fire
    [SerializeField] private GameObject[] bulletList;
    [SerializeField] private int currenTierBullet;
    private bool checkPresent = false;
    [SerializeField, Range(0, 1)] private float timeFire;

    private float nextTimeFire = 0f;

    private AudioManager audioManager;
    private int health;

    public float blinkInterval = 0.2f; // Khoảng thời gian giữa các nhấp nháy
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkTime;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        this.health = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ShipMovement();
        checkPresent = false;
        Fire();

    }

    void ShipMovement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Clamp(mousePosition.x, Camera.main.ViewportToWorldPoint(Vector2.zero).x, Camera.main.ViewportToWorldPoint(Vector2.one).x);
        mousePosition.y = Mathf.Clamp(mousePosition.y, Camera.main.ViewportToWorldPoint(Vector2.zero).y, Camera.main.ViewportToWorldPoint(Vector2.one).y);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeFire)
        {
            nextTimeFire = Time.time + timeFire;
            audioManager.PlayFire(audioManager.fireClip);

            if (currenTierBullet == 0 || currenTierBullet == 1 || currenTierBullet == 4 || currenTierBullet == 5)
                Instantiate(bulletList[currenTierBullet], transform.position, Quaternion.identity);
            else if (currenTierBullet == 2 || currenTierBullet == 3)
            {
                Instantiate(bulletList[currenTierBullet], transform.position, Quaternion.identity);
                Instantiate(bulletList[0], transform.position, Quaternion.Euler(0, 0, 10f));
                Instantiate(bulletList[0], transform.position, Quaternion.Euler(0, 0, -10f));
            }
            else
            {
                Instantiate(bulletList[currenTierBullet], transform.position, Quaternion.identity);
                Instantiate(bulletList[4], transform.position, Quaternion.Euler(0, 0, 10f));
                Instantiate(bulletList[4], transform.position, Quaternion.Euler(0, 0, -10f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Present" && currenTierBullet < 7 && !checkPresent)
        {
            checkPresent = true;
            audioManager.PlayLevelUp(audioManager.levelUpAudioClip);
            this.currenTierBullet += 1;
            this.timeFire -= 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !shield.isShield && (collision.gameObject.tag == "RedChicken" || collision.gameObject.tag == "Egg" || collision.gameObject.tag == "Rock"))
        {
            if (health > 0)
            {
                this.health--;
                HeathController.instance.getHeath(health);
                shield.Show();
                StartBlinking();
                Invoke("StopBlinking", blinkTime);
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
            }
            else
            {
                Destroy(gameObject);
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
                audioManager.PlayBackground(audioManager.gameOverClip);

            }
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var expl = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(expl, 0.3f);
        }
    }

    public void StartBlinking()
    {
        StartCoroutine(Blink());
    }

    public void StopBlinking()
    {
        StopCoroutine(Blink());
        spriteRenderer.enabled = true;
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0f;
        while (elapsedTime < blinkTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        spriteRenderer.enabled = true;
    }
}
