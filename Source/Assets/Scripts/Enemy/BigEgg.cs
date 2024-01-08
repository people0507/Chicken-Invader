using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BigEgg : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject spawnChicken;
    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BottomBorder")
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                Destroy(gameObject);
                Chicken chicken = Instantiate(spawnChicken, transform.position, Quaternion.identity).GetComponent<Chicken>();
                chicken.setIsMoving(true);
            }
            Atomic atomic = collision.GetComponent<Atomic>();
            if (atomic != null)
            {
                Destroy(gameObject);
                Chicken chicken = Instantiate(spawnChicken, transform.position, Quaternion.identity).GetComponent<Chicken>();
                chicken.setIsMoving(true);
            }
        }
    }
}
