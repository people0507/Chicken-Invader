using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
        
    public void PlayMainMenu()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void PlayNewGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void PlayLevel2()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
