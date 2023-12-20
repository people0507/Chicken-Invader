using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    private int score;
    public static ScoreController instance;

    private void Awake()
    {
        instance = this;
    }

    public void getScore(int score)
    {
        this.score += score;
        textScore.text = "Score : " + this.score.ToString();
    }
}
