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
    private float rotate;

    private AudioManager audioManager;
    
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        Destroy(gameObject,aliveTimeChickenLeg);
        rotate = Random.Range(-10, 10);
    }
    

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotate);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với mặt đất
        if (collision.gameObject.CompareTag("BottomBorder"))
        {
            countJump++;
            if(countJump == 3)
            {
                myBody.velocity = new Vector2(0,0);
                this.rotate = 0;
            }
            else
            {
                myBody.AddForce(Vector2.up * (jumpForce -=1), ForceMode2D.Impulse);
            } 
        }

        
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            audioManager.PlayEat(audioManager.eatClip);
            Destroy(gameObject);
        }
    }
}
