using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutronBullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private AudioManager audioManager;
    private Vector3 pos;
    private bool moveToPos;
    private void Awake()
    {
        //myBody = GetComponent<Rigidbody2D>();
        this.moveToPos = false;
    }

    private void FixedUpdate()
    {
        //myBody.velocity = new Vector2(0f, -speed);
        float x = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1;
        float y = Camera.main.ViewportToWorldPoint(Vector3.one).y + 1;
        //Debug.Log("x = " + x + ", y = " + y);
        if (transform.position.x > x || transform.position.x < -x || transform.position.y > y || transform.position.y < -y)
        {
            Destroy(gameObject);
        }
        if (moveToPos)
            transform.Translate(pos.normalized * Time.deltaTime * speed);
        else
            transform.Translate(new Vector2(0, -speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void MoveToPos(Vector3 posShip)
    {
        this.moveToPos = true;
        this.pos = posShip - transform.position;
    }
}
