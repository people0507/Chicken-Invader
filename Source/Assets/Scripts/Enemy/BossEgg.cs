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
    private int currentIndex;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
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
}