using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullet1 : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, bulletSpeed);
    }
}
