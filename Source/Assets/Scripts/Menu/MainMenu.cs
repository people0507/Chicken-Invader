using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void PlayNewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
