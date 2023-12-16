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
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        AudioSource audioSource = GetComponent<AudioSource>();

        //Kiểm tra nếu AudioSource tồn tại và có AudioClip
        if (audioSource != null && audioSource.clip != null)
        {
            //Phát âm thanh
            audioSource.Play();
        }

        Vector3 temp = transform.position;
        temp.y -= 0.6f;
        Instantiate(egg, temp, Quaternion.identity);
        StartCoroutine(EnemyShoot());

    }
    private void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            Destroy(target.gameObject);
        }

        if (target.tag == "Bullet")
        {
            Instantiate(chickenleg, transform.position, transform.rotation);
            Destroy(target.gameObject);
        }
    }
}
