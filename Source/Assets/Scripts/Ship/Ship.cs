using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D myBody;
    private void Awake()
    {
     myBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
    ShipMovement();
    }
    void ShipMovement()
    {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
}
