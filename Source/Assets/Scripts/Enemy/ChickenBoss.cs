using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChickenBoss : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;
    [SerializeField] private GameObject fog;
    [SerializeField] private int score;
    [SerializeField] private float hp;

    //private float x, y;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShoot());
        StartCoroutine(MoveBossToRandom());
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

    private IEnumerator MoveBossToRandom()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime * 4);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveBossToRandom());
    }

    private Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    //public void MoveToPos(float posX, float posY)
    //{
    //    this.x = posX;
    //    this.y = posY;
    //    transform.Translate(new Vector3(0, y - transform.position.y) * Time.deltaTime * speed);
    //}


    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            Instantiate(egg, transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity);
            Instantiate(egg, transform.position - new Vector3(1, 0.6f, 0), Quaternion.identity);
            Instantiate(egg, transform.position - new Vector3(-1, 0.6f, 0), Quaternion.identity);
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
                int ranLeg = Random.Range(10, 15);
                for(int i=0; i<ranLeg; i++){
                    Instantiate(chickenleg, transform.position, transform.rotation);
                }

                audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
                audioManager.PlayBackground(audioManager.gameWinClip);
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
