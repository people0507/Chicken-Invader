using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class Ship : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    private Shield shield;
    private bool isControl = true;

    //fire
    [SerializeField] private GameObject[] bulletList;
    [SerializeField] private int currenTierBullet;
    private bool checkPresent = false;
    [SerializeField, Range(0, 1)] private float timeFire;

    private float nextTimeFire = 0f;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;

    private int health;

    private float blinkInterval = 0.2f;
    [SerializeField] private float blinkTime;

    private Canvas canvasOver;
    [SerializeField] private float timeShowCanvas;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        this.health = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvasOver = GameObject.Find("GameOver").GetComponent<Canvas>();
    }

    void Update()
    {
        if (isControl)
        {
            ShipMovement();
            Fire();
        }
        checkPresent = false;
    }

    void ShipMovement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = Mathf.Clamp(mousePosition.x, Camera.main.ViewportToWorldPoint(Vector2.zero).x, Camera.main.ViewportToWorldPoint(Vector2.one).x);
        mousePosition.y = Mathf.Clamp(mousePosition.y, Camera.main.ViewportToWorldPoint(Vector2.zero).y, Camera.main.ViewportToWorldPoint(Vector2.one).y);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    public void setControl(bool control)
    {
        this.isControl = control;
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeFire)
        {
            nextTimeFire = Time.time + timeFire;
            audioManager.PlayFire(audioManager.fireClip);

            if (currenTierBullet <= 6)
                Instantiate(bulletList[currenTierBullet], transform.position, Quaternion.identity);
            else
            {
                switch (currenTierBullet)
                {
                    case 7:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 9:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 10:
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 11:
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 12:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[8], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 13:
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[8], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 14:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 15:
                        Instantiate(bulletList[6], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[8], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[6], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 16:
                        Instantiate(bulletList[8], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[8], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[7], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[8], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 17:
                        Instantiate(bulletList[9], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 18:
                        Instantiate(bulletList[10], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 19:
                        Instantiate(bulletList[9], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[9], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 20:
                        Instantiate(bulletList[9], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[9], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 21:
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.25f, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[9], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.25f, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 22:
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 23:
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        break;
                    case 24:
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.25f, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.25f, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 25:
                        Instantiate(bulletList[9], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[9], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 26:
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        break;
                    case 27:
                        Instantiate(bulletList[10], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[10], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f, transform.position.z), Quaternion.identity);
                        break;
                    case 28:
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
                        break;
                    default:
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.5f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x - 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.25f, transform.position.y - 0.1f, transform.position.z), Quaternion.identity);
                        Instantiate(bulletList[11], new Vector3(transform.position.x + 0.5f, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Present" && currenTierBullet < 28 && !checkPresent)
        {
            checkPresent = true;
            audioManager.PlayLevelUp(audioManager.levelUpAudioClip);
            this.currenTierBullet += 1;
            this.timeFire -= 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isControl)
            return;
        if ( !shield.isShield && (collision.gameObject.tag == "Chicken" || collision.gameObject.tag == "Egg" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "BossChicken"))
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
                var expl = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(expl, 0.3f);

                Renderer renderer = GetComponent<Renderer>();
                renderer.sortingOrder = -10;
                setControl(false);

                audioManager.PlayShipDead(audioManager.shipDeadAudioClip);
                audioManager.PlayBackground(audioManager.gameOverClip);
                Time.timeScale = 0.2f;

                Invoke("DestroyShip", timeShowCanvas);
            }
        }
    }

    private void DestroyShip()
    {
        canvasOver.setActiveTrue();
        Destroy(gameObject);
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
