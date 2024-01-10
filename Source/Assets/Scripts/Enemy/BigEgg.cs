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
    private bool destroyed = true;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
        float yMin = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
        if (transform.position.y < yMin)
        {
            destroyed = false;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (destroyed)
        {
            Chicken chicken = Instantiate(spawnChicken, transform.position, Quaternion.identity).GetComponent<Chicken>();
            chicken.setMoveRandom();
            //chicken.setMoveLemniscate(0, 0);
        }
    }
}
