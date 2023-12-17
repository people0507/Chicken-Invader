using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RedChicken : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;
    [SerializeField] private float changeDirectionInterval = 3f;
    private float timer;
    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public float aliveTimeChicken;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,aliveTimeChicken);
        timer = changeDirectionInterval;
        GetRandomDirection();

        audioSource = GetComponent<AudioSource>(); // Lấy thành phần AudioSource từ GameObject hiện tại
        audioSource.clip = audioClip; // Gắn audio clip vào AudioSource
        audioSource.Play(); // Phát âm thanh

        StartCoroutine(EnemyShoot());
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            GetRandomDirection();
            timer = changeDirectionInterval;
        }
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
            
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Instantiate(chickenleg, transform.position, transform.rotation);

            int random = Random.Range(1, 5);
            if(random == 3)
                Instantiate(present, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
