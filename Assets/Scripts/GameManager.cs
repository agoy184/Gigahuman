using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private GameObject player;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public static bool isPaused;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        pauseMenu.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }    
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Die()
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

}
