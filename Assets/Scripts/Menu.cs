using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void ResumeGame()
    {
        GameManager.Instance.Resume();
    }

    public void RestartGame()
    {
        GameManager.Instance.Restart();
    }

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
    }

    public void EnablePlayer()
    {
        GameManager.Instance.EnablePlayer();
    }
}
