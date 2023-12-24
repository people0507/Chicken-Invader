using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RedChicken : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;
    [SerializeField] private float changeDirectionInterval = 2f;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;
    [SerializeField] private int score;
    [SerializeField] private float hp;
    
    private AudioManager audioManager;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetRandomDirection();
        StartCoroutine(EnemyShoot());
    }

    void FixedUpdate()
    {
        if (Time.time >= changeDirectionInterval)
        {
            GetRandomDirection();
            changeDirectionInterval += changeDirectionInterval;
        }
        Vector3 checkPos = transform.position;
        checkPos.x = Mathf.Clamp(checkPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
        transform.position = checkPos;

        float yMin = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        if (transform.position.y <= yMin)
            Destroy(gameObject, 1f);
    }

    private void GetRandomDirection()
    {
        myBody.velocity = new Vector2(Random.Range(-1f, 1f), -speed);
    }

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            Instantiate(egg, transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity);
            audioManager.PlayEgg(audioManager.eggClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if(bullet != null) 
            {
                hp -= bullet.getDameBullet();
                audioManager.PlayChickenHurt(audioManager.chickenHurtAudioClip);
            }
            if(hp <= 0)
            {
                Instantiate(chickenleg, transform.position, transform.rotation);

                audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
                int random = Random.Range(1, 5);
                if(random == 3)
                    Instantiate(present, transform.position, transform.rotation);
                ScoreController.instance.getScore(score);
                Destroy(gameObject);
            }
        }
    }
}
