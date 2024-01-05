using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Egg : MonoBehaviour
{
    [SerializeField] private float speed;
    //private Rigidbody2D myBody;
    [SerializeField] private GameObject eggBreak;

    private AudioManager audioManager;
    private void Awake()
    {
        //myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(0, -speed * Time.deltaTime));
        //myBody.velocity = new Vector2(0f, -speed);
        float x = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1;
        float y = Camera.main.ViewportToWorldPoint(Vector3.one).y + 1;
        Debug.Log("x = " + x + ", y = " + y);
        if (transform.position.x > x || transform.position.x < -x || transform.position.y > y || transform.position.y < -y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BottomBorder")
        {
            var eggBr = Instantiate(eggBreak, transform.position, transform.rotation);
            Destroy(eggBr, 0.3f);
            audioManager.PlayEggBreak(audioManager.eggBreakClip);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
