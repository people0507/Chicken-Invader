﻿using System.Collections;
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

    private AudioManager audioManager;
    public float aliveTimeChicken;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
            Instantiate(chickenleg, transform.position, transform.rotation);

            audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
            int random = Random.Range(1, 5);
            if(random == 3)
                Instantiate(present, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
