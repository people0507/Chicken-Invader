using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Egg : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myBody;
    [SerializeField] private GameObject eggBreak;
    public float aliveTimeEgg;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,aliveTimeEgg);
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BottomBorder")
        {
            Instantiate(eggBreak, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
