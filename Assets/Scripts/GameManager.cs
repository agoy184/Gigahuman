using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private GameObject player;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public static bool isPaused;
    private GameObject gun;

    public bool isParallelDimension = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
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
        this.gun = player.GetComponent<PlayerController>().GetGun();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void TravelToParallelDimension(string sceneToLoad)
    {
        isParallelDimension = true;
        gun.GetComponent<Gun>().Evolve();
        SceneManager.LoadScene(sceneToLoad);
        player.transform.position = new Vector3(0, 0, 0);
    }

}
