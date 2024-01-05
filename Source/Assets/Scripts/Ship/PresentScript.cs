using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentScript : MonoBehaviour
{
    [SerializeField] private int score;

    private void Update()
    {
        float yMin = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        if(transform.position.y < yMin - 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Player")
        {
            Destroy(gameObject);
            ScoreController.instance.getScore(score);
        }
    }
}
