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

    private AudioManager audioManager;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
            audioManager.PlayEggBreak(audioManager.eggBreakClip);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
