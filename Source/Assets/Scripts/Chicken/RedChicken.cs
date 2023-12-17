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
        yield return new WaitForSeconds(Random.Range(2f, 6f));

        Vector3 temp = transform.position;
        temp.y -= 0.6f;
        Instantiate(egg, temp, Quaternion.identity);

        StartCoroutine(EnemyShoot());
         AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Instantiate(chickenleg, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
