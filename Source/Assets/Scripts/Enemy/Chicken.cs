using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;
    [SerializeField] private GameObject fog;

    //move with Lemniscate
    private bool moveLemniscate = false;
    [SerializeField] private float radius = 4f;
    private float angle = 0f;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        StartCoroutine(EnemyShoot());

    }

    void FixedUpdate()
    {
        //Vector3 checkPos = transform.position;
        //checkPos.x = Mathf.Clamp(checkPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
        //checkPos.y = Mathf.Clamp(checkPos.y, Camera.main.ViewportToWorldPoint(Vector3.zero).y + 4f, Camera.main.ViewportToWorldPoint(Vector3.one).y);
        //transform.position = checkPos;
    }

    //move to position
    public void setMoveToPos(float posX, float posY)
    {
        StartCoroutine(MoveToPos(posX, posY));
    }
    private IEnumerator MoveToPos(float posX, float posY)
    {
        while (transform.position != new Vector3(posX, posY, 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, posY, 0), speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (moveLemniscate)
            StartCoroutine(MoveLemniscate(posX, posY));
    }

    //move random
    public void setMoveRandom()
    {
        StartCoroutine(MoveToRandom());
    }
    private IEnumerator MoveToRandom()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime * 2);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveToRandom());
    }
    private Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    //move with Lemniscate
    public void setMoveLemniscate(float posX, float posY)
    {
        this.moveLemniscate = true;
        StartCoroutine(MoveToPos(posX, posY));
    }
    private IEnumerator MoveLemniscate(float posX, float posY)
    {
        while (true)
        {
            angle += Time.deltaTime * speed;

            float x = radius * Mathf.Cos(angle) / (1f + Mathf.Sin(angle) * Mathf.Sin(angle));
            float y = x * Mathf.Sin(angle);

            transform.position = new Vector3(x + posX, y + posY, 0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
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

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(chickenleg, transform.position, transform.rotation);
            int random = Random.Range(1, 5);
            if (random == 3)
                Instantiate(present, transform.position, transform.rotation);
            var Fog = Instantiate(fog, transform.position, transform.rotation);
            Destroy(Fog, 0.2f);
        }
    }
}
