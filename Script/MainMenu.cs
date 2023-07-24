using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject highScorePanel;

    private GameManager gameManager;

    public Text highScoreText;


    private void Awake()
    {

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GAME");
    }

    public void OnApplicationQuit()
    {
        Application.Quit(); 
    }

    public void highScore()
    {
        highScorePanel.SetActive(true);
    }

    public void close()
    {
        highScorePanel.SetActive(false);
    }

}
