using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class BossEgg : MonoBehaviour
{
    [SerializeField] private float currentHP;
    [SerializeField] private float hp;
    [SerializeField] private GameObject[] eggGameObjects;
    [SerializeField] private GameObject neutronBullet;
    [SerializeField] private float bulletCount;
    [SerializeField] private float timeFire;
    [SerializeField] private float speed;
    private int currentIndex;
    private AudioManager audioManager;

    [SerializeField] private float shake;
    private bool isShake = false;
    private Vector3 currentPos;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentIndex = Array.IndexOf(eggGameObjects, gameObject);
        if(currentIndex == eggGameObjects.Length - 1)
        {
            StartCoroutine(MoveToRandom());
            StartCoroutine(EnemyShootFinal());
        }
        else
        {
            StartCoroutine(MoveStart());
            StartCoroutine(EnemyShoot());
        }
    }

    private void Update()
    {
        currentPos = transform.position;
        if (currentHP <= hp)
        {
            if (currentIndex + 1 < eggGameObjects.Length)
            {
                currentIndex += 1;
                eggGameObjects[currentIndex].SetActive(true);
                eggGameObjects[currentIndex].GetComponent<BossEgg>().setCurrentHp(currentHP);
                setPosition(transform.position);
                gameObject.SetActive(false);
            }
            else 
                Destroy(eggParent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                currentHP -= bullet.getDameBullet();
                audioManager.PlayChickenHurt(audioManager.chickenHurtAudioClip);
                if(isShake)
                    StartCoroutine(Shake1());
            }
            //if (hp <= 0)
            //{
            //    var Fog = Instantiate(fog, transform.position, transform.rotation);
            //    Destroy(Fog, 0.2f);
            //    int ranLeg = Random.Range(10, 15);

            //    for (int i = 0; i < ranLeg; i++)
            //    {
            //        Instantiate(chickenleg, transform.position, transform.rotation);
            //    }

            //    Destroy(gameObject);
            //    audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
            //    audioManager.PlayBackground(audioManager.gameWinClip);
            //    ScoreController.instance.getScore(score);
            //}
        }
    }

    private IEnumerator MoveStart()
    {
        while (transform.position != Vector3.up)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.up, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        isShake = true;
    }

    private IEnumerator Shake1()
    {
        Vector3 pos1 = currentPos + new Vector3(0, shake, 0);
        Vector3 pos2 = currentPos;
        while (transform.position.y != pos1.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1, Time.deltaTime * 20);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(Shake2(pos2));
    }

    private IEnumerator Shake2(Vector3 pos2)
    {
        while(transform.position.y != pos2.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2, Time.deltaTime * 20);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    private IEnumerator MoveToRandom()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
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

    public void setCurrentHp(float hp)
    {
        this.currentHP = hp;
    }

    public void setPosition(Vector3 pos)
    {
        eggGameObjects[currentIndex].transform.position = pos;
    }

    private IEnumerator EnemyShoot()
    {
        while (true)
        {
            float angleIncrement = 360f / bulletCount;

            // Bắn nhiều viên đạn theo các hướng khác nhau
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleIncrement;
                Vector3 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector3.down;

                Instantiate(neutronBullet, transform.position - new Vector3(0, 0.6f, 0), Quaternion.LookRotation(Vector3.forward, bulletDirection));
            }

            // Thời gian chờ giữa các lần bắn
            yield return new WaitForSeconds(timeFire);
        }
    }

    private IEnumerator EnemyShootFinal()
    {
        while (true)
        {
            Ship ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
            NeutronBullet neutron = Instantiate(neutronBullet, transform.position - new Vector3(0, 0.6f, 0), Quaternion.identity).GetComponent<NeutronBullet>();
            neutron.MoveToPos(ship.getPosShip());
            yield return new WaitForSeconds(timeFire);
        }
    }
}