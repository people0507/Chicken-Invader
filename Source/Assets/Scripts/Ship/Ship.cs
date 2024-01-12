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
    [SerializeField] private GameObject nuclear;
    [SerializeField] private int countNuclear;

    private float nextTimeFire = 0f;

    private AudioManager audioManager;
    private SpriteRenderer spriteRenderer;

    private int health;

    private float blinkInterval = 0.2f;
    [SerializeField] private float blinkTime;

    private Canvas canvasOver;
    [SerializeField] private float timeShowCanvas;

    private int sceneBuildIndex;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        this.health = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvasOver = GameObject.Find("GameOver").GetComponent<Canvas>();
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneBuildIndex = currentScene.buildIndex;
        //Debug.Log("Scene Build Index: " + sceneBuildIndex);
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
        if (Input.GetMouseButtonDown(1) && countNuclear > 0)
        {
            Instantiate(nuclear, transform.position, Quaternion.identity);
            countNuclear--;
            Debug.Log(countNuclear);
            NuclearController.instance.getNuclear(countNuclear);
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeFire)
        {
            nextTimeFire = Time.time + timeFire;
            audioManager.PlayFire(audioManager.fireClip);

            if (sceneBuildIndex == 1)
            {
                switch (currenTierBullet)
                {
                    case 0:
                        Instantiate(bulletList[0], transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 7:
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                    case 9:
                        Instantiate(bulletList[0], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[0], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                    case 10:
                        Instantiate(bulletList[2], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                    default:
                        Instantiate(bulletList[2], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[1], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[2], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                }
            }
            else
            {
                switch (currenTierBullet)
                {
                    case 0:
                        Instantiate(bulletList[3], transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(bulletList[4], transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(bulletList[3], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[3], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(bulletList[3], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[3], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.25f, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[3], transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.25f, 0.25f, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        break;
                    case 7:
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.25f, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.25f, 0.25f, 0), Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(bulletList[3], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[3], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                    case 9:
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.5f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.5f, -0.1f, 0), Quaternion.identity);
                        break;
                    case 10:
                        Instantiate(bulletList[4], transform.position + new Vector3(-0.5f, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[4], transform.position + new Vector3(0.5f, -0.25f, 0), Quaternion.identity);
                        break;
                    case 11:
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.5f, -0.2f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.5f, -0.2f, 0), Quaternion.identity);
                        break;
                    default:
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.5f, -0.2f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(-0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0, -0.25f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.25f, -0.1f, 0), Quaternion.identity);
                        Instantiate(bulletList[5], transform.position + new Vector3(0.5f, -0.2f, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Present" && currenTierBullet < 11 && !checkPresent)
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
        if (!shield.isShield && (collision.gameObject.tag == "Chicken" || collision.gameObject.tag == "Egg" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "BossChicken" || collision.gameObject.tag == "BigEgg"))
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

                Invoke("ShowCanvas", timeShowCanvas);
            }
        }
    }

    private void ShowCanvas()
    {
        Cursor.visible = true;
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
