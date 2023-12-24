using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(1, -1, 0) * Time.deltaTime * speed;
        transform.position += newPos;
        float xMax = Camera.main.ViewportToScreenPoint(Vector2.one).x;
        if (transform.position.x > xMax + 1)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if(bullet != null)
            {
                hp -= bullet.getDameBullet();
                audioManager.PlayRockHurt(audioManager.rockHurtAudioClip);
            }
            if (hp <= 0)
            {
                Destroy(gameObject);
                audioManager.PlayRockDeath(audioManager.rockDeathAudioClip);
            }
        }
    }
}
