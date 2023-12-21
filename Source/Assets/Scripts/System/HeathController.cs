using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeathController : MonoBehaviour
{
    [SerializeField] TMP_Text textHeath;
    public static HeathController instance;

    private void Awake()
    {
        instance = this;
    }

    public void getHeath(int health)
    {
        textHeath.text = health.ToString();
    }
}
