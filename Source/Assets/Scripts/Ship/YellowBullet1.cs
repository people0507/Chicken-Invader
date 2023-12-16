using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet1 : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D myBody;
    public float aliveTimeBullet;
    public GameObject fog;

    private void Start()
    {
        Destroy(gameObject, aliveTimeBullet);
    }
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, bulletSpeed);
    }
    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "RedChicken")
        {
            Destroy(target.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "RedChicken")
    {
        Instantiate(fog, transform.position, transform.rotation);
    }
}
}
