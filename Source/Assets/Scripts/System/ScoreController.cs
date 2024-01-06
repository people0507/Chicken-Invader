using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text textScore;
    private int score;
    public static ScoreController instance;

    private void Awake()
    {
        instance = this;
    }

    public void getScore(int score)
    {
        this.score += score;
        textScore.text = this.score.ToString("#,#");
    }
}
