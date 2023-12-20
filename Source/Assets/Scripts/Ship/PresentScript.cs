using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentScript : MonoBehaviour
{
    [SerializeField] private int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Player")
        {
            ScoreController.instance.getScore(score);
            Destroy(gameObject);
        }
    }

}
