using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLeg : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myBody;
    public float aliveTimeChickenLeg;
    public float jumpForce;
    private float countJump;


    private void Start()
    {
        Destroy(gameObject,aliveTimeChickenLeg);
    }
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
    }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Kiểm tra va chạm với mặt đất
            if (collision.gameObject.CompareTag("BottomBorder"))
            {
            countJump++;
            if(countJump == 3)
            {
                myBody.velocity = new Vector2 (0,0);
            }
            else
            {
            myBody.AddForce(Vector2.up * (jumpForce -=1), ForceMode2D.Impulse);
            }
            }
        }
}
