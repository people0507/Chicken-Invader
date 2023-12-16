using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
    }
}
