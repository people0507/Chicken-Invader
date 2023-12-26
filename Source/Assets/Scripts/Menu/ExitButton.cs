using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private bool isPanelActive = false;
    [SerializeField] private GameObject canvasExit;
    private AudioManager audioManager;
    private void Awake()
    {
        canvasExit.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPanelActive)
            {
                // audioManager.StopBackgroundAudio();
                isPanelActive = true;
                Time.timeScale = 0f;
                canvasExit.SetActive(true);
            }
            else
            {
                // audioManager.PlayBackground(audioManager.backgroundClip);
                isPanelActive = false;
                Time.timeScale = 1f;
                canvasExit.SetActive(false);
            }
        }
    }
}
