using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    [SerializeField] private GameObject present;
    [SerializeField] private int score;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos =new Vector3(1, -1, 0) * Time.deltaTime * speed;
        transform.position += pos;

        float yMin = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        float xMax = Camera.main.ViewportToWorldPoint(Vector2.one).x;
        if (transform.position.y < yMin - 1 || transform.position.x > xMax + 1)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                hp -= bullet.getDameBullet();
                audioManager.PlayRockHurt(audioManager.rockHurtAudioClip);
            }
            Atomic atomic = collision.GetComponent<Atomic>();
            if (atomic != null)
            {
                hp -= atomic.getDameBullet();
                audioManager.PlayRockHurt(audioManager.rockHurtAudioClip);
            }
            if (hp <= 0)
            {
                int random = Random.Range(1, 5);
                if (random == 3)
                Instantiate(present, transform.position, transform.rotation);
                ScoreController.instance.getScore(score);
                Destroy(gameObject);
                audioManager.PlayRockDeath(audioManager.rockDeathAudioClip);
            }
        }
    }
    public void setSpeed(float spe)
    {
        this.speed = spe;
    }
}
