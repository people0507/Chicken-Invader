using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private float hp;
    [SerializeField] private bool useAudio;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                hp -= bullet.getDameBullet();
                if(useAudio)
                    audioManager.PlayChickenHurt(audioManager.chickenHurtAudioClip);
            }
            Atomic atomic = collision.GetComponent<Atomic>();
            if (atomic != null)
            {
                hp -= atomic.getDameBullet();
                if (useAudio)
                    audioManager.PlayChickenHurt(audioManager.chickenHurtAudioClip);
            }
            if (hp <= 0)
            {
                Destroy(gameObject);
                if (useAudio)
                    audioManager.PlayChickenDeath(audioManager.chickenDeathAudioClip);
                ScoreController.instance.getScore(score);
            } 
        }
    }
}
