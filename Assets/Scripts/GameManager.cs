using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject[] indesctructibleObjects;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private bool playSceneLoaded = false;

    public bool isPaused = false;

    private GameObject player;
    private GameObject gun;

    public bool isParallelDimension = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene")
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }

            if (!playSceneLoaded)
            {
                if (player != null)
                {
                    player.SetActive(true);
                }
                AudioManager.Instance.PlayMusic("Retro Music");
                Cursor.lockState = CursorLockMode.Locked;

                playSceneLoaded = true;
            }
        } 
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

        AudioManager.Instance.PlayMusic("Retro Music");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HNScene");
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
