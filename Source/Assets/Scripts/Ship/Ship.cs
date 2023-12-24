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
    [SerializeField] private Shield shield;
    private bool hasIncreasedTier = false;

    //fire
    [SerializeField] private GameObject[] bulletList;
    [SerializeField] private int currenTierBullet;
    [SerializeField, Range(0, 1)] private float timeFire;

    private float nextTimeFire = 0f;

    private AudioManager audioManager;
    private int health;

    public float blinkInterval = 0.2f; // Khoảng thời gian giữa các nhấp nháy
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        this.health = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        ShipMovement();
        hasIncreasedTier = false;
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
        if (collision.gameObject.tag == "Present" && currenTierBullet < 7 && !hasIncreasedTier)
        {
            audioManager.PlayLevelUp(audioManager.levelUpAudioClip);
            this.currenTierBullet += 1;
            this.timeFire -= 0.01f;
            hasIncreasedTier = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !shield.isShield && (collision.gameObject.tag == "RedChicken" || collision.gameObject.tag == "Egg" || collision.gameObject.tag == "Rock"))
        {
            if (health > 0)
            {
                this.health--;
                Instantiate(explosion, transform.position, transform.rotation);
                shield.Show();
                HeathController.instance.getHeath(health);
                StartBlinking();
                Invoke("StopBlinking", 3f);
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
            }
            else
            {
                Destroy(gameObject);
                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
                audioManager.PlayBackground(audioManager.gameOverClip);
                Time.timeScale = 0.1f;
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(Blink());
            spriteRenderer.enabled = true; // Đảm bảo đối tượng được hiển thị khi dừng nhấp nháy
        }
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0f; // Thời gian đã trôi qua
        while (isBlinking && elapsedTime < 3f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        spriteRenderer.enabled = true; // Đảm bảo đối tượng được hiển thị khi kết thúc nhấp nháy
    }
}
