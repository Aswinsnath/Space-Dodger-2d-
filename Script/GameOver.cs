using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
  public void Restart()
    {
        SceneManager.LoadScene("GAME");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
