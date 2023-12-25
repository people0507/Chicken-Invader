﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RedChicken : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject chickenleg;
    [SerializeField] private GameObject present;
    [SerializeField] private int score;
    [SerializeField] private float hp;

    private float x, y;
    
    private AudioManager audioManager;

    void Awake()
    {
        x = transform.position.x;
        y= transform.position.y;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShoot());
    }

    void FixedUpdate()
    {
        Vector3 checkPos = transform.position;
        checkPos.x = Mathf.Clamp(checkPos.x, Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
        transform.position = checkPos;

        Move(x, y);

        float yMin = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        if (transform.position.y <= yMin)
            Destroy(gameObject, 1f);
    }

    public void Move(float posX, float posY)
    {
        this.x = posX;
        this.y = posY;
        transform.Translate(new Vector3(x - transform.position.x, y - transform.position.y) * Time.deltaTime * speed);
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
