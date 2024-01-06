using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossEgg : MonoBehaviour
{
    [SerializeField] private float currentHP;
    [SerializeField] private float hp;
    [SerializeField] private GameObject[] eggGameObjects;
    [SerializeField] private GameObject eggBullet;
    [SerializeField] private float bulletCount;
    private int currentIndex;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(EnemyShoot());
        currentIndex = Array.IndexOf(eggGameObjects, gameObject);
        Debug.Log("Vị trí hiện tại của GameObject là: " + currentIndex);
    }

    private void Update()
    {
        if (currentHP <= hp)
        {
            if (currentIndex + 1 < eggGameObjects.Length)
            {
                currentIndex += 1;
                eggGameObjects[currentIndex].SetActive(true);
                eggGameObjects[currentIndex].GetComponent<BossEgg>().setCurrentHp(currentHP);
                gameObject.SetActive(false);
            }
            else
                Destroy(gameObject);

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

    public void setCurrentHp(float hp)
    {
        this.currentHP = hp;
    }

      IEnumerator EnemyShoot()
    {
        while (true)
        {

            float angleIncrement = 360f / bulletCount;

            // Bắn nhiều viên đạn theo các hướng khác nhau
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleIncrement;
                Vector3 bulletDirection = Quaternion.Euler(0, 0, angle) * Vector3.down;

                Instantiate(eggBullet, transform.position - new Vector3(0, 0.6f, 0), Quaternion.LookRotation(Vector3.forward, bulletDirection));
            }

            // Thời gian chờ giữa các lần bắn
            yield return new WaitForSeconds(2f);
        }
    }
}