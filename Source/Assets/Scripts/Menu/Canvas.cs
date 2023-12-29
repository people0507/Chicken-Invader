using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void setActiveFalse()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void setActiveTrue()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
}
