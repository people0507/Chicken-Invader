using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private float yMax;
    [SerializeField] private float dameBullet;
    void Start()
    {
        yMax = Camera.main.ViewportToWorldPoint(Vector2.one).y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (transform.position.y > yMax)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Chicken" || collision.gameObject.tag == "Rock" || collision.gameObject.tag == "BossChicken" || collision.gameObject.tag == "BigEgg")
        {
            Destroy(gameObject);
        }
    }

    public float getDameBullet()
    {
        return dameBullet;
    }
}
