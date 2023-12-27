using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BigEgg : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
            Destroy(gameObject);
        }
    }
}
