using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private bool isPanelActive = false;
    [SerializeField] private GameObject canvasExit;
    private AudioManager audioManager;
    private Ship ship;

    private void Awake()
    {
        canvasExit.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPanelActive)
            {
                audioManager.StopAllAudioSources();
                isPanelActive = true;
                Time.timeScale = 0f;
                canvasExit.SetActive(true);
                ship.setMove(false);
            }
            else
                Continue();
        }
    }
    public void Continue()
    {
        audioManager.ResumeAllAudiioSources();
        isPanelActive = false;
        Time.timeScale = 1f;
        canvasExit.SetActive(false);
        ship.setMove(true);
    }
}
