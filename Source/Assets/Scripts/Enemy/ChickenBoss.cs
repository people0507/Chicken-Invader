using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChickenBoss : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject fog;
    [SerializeField] private bool spawnEgg;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(spawnEgg)
            StartCoroutine(EnemyShoot());
        StartCoroutine(MoveBossToRandom());
    }

    void FixedUpdate()
    {
    }

    private IEnumerator MoveBossToRandom()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveBossToRandom());
    }

    private Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.6f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }

    IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            for(int i=-1; i<=1; i++)
            {
                Instantiate(egg, transform.position - new Vector3(i, -0.6f, 0), Quaternion.identity);
            }
            audioManager.PlayEgg(audioManager.eggClip);
        }
    }

    private void OnDestroy()
    {
        //audioManager.PlayBackground(audioManager.gameWinClip);
        var Fog = Instantiate(fog, transform.position, transform.rotation);
        Destroy(Fog, 0.2f);
        int ranLeg = Random.Range(10, 15);

        for (int i = 0; i < ranLeg; i++)
        {
            Instantiate(chickenleg, transform.position, transform.rotation);
        }
    }
}
