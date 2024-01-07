using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeathController : MonoBehaviour
{
    [SerializeField] Text textHeath;
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
