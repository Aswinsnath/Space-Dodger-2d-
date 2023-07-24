using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pauseMenuUI;

    public Button pauseButton;

    void Start()
    {
        
        pauseButton.onClick.AddListener(TogglePause);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) || IsPauseButtonClicked())
        {
            TogglePause();
        }
    }

    bool IsPauseButtonClicked()
    {
        
        return false;
    }

    void TogglePause()
    {
        if (GameisPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
