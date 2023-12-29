using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChickenSpawnEgg : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;
    [SerializeField] private GameObject fog;
    [SerializeField] private int score;
    [SerializeField] private float hp;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShoot());
        StartCoroutine(MoveChickenToRandom());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 checkPos = transform.position;
        checkPos.x = Mathf.Clamp(checkPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
        transform.position = checkPos;

        //x = transform.position.x;
        //y = transform.position.y;
        //MoveToPos(x, y);
    }

    private IEnumerator MoveChickenToRandom()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime * 4);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveChickenToRandom());
    }

    private Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            Instantiate(egg, transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity);
            audioManager.PlayEgg(audioManager.eggClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                hp -= bullet.getDameBullet();
                audioManager.PlayChickenHurt(audioManager.chickenHurtAudioClip);
            }
            if (hp <= 0)
            {
                Instantiate(chickenleg, transform.position, transform.rotation);

                audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
                int random = Random.Range(1, 5);
                if (random == 3)
                    Instantiate(present, transform.position, transform.rotation);
                ScoreController.instance.getScore(score);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var Fog = Instantiate(fog, transform.position, transform.rotation);
            Destroy(Fog, 0.2f);
        }
    }
}
