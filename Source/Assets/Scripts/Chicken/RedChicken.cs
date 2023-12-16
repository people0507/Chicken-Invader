using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RedChicken : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;
    [SerializeField] private float changeDirectionInterval = 3f; // Thời gian đổi hướng
    private float timer;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = changeDirectionInterval;
        GetRandomDirection();
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            GetRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    private void GetRandomDirection()
    {
        myBody.velocity = new Vector2(Random.Range(-1f, 1f), -speed);
    }
}
