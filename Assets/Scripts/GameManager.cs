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
    public bool isPaused = false;

    private GameObject player;
    private GameObject gun;

    public bool isParallelDimension = false;

    public bool isWon = false;

    void Start() {
        AudioManager.Instance.PlayMusic("MainMenu");
    }

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
        if (SceneManager.GetActiveScene().name != "MenuScene" && SceneManager.GetActiveScene().name != "MenuScene2" && SceneManager.GetActiveScene().name != "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }

            // if on click, and the game is not paused, hide the cursor if it isn't already
            if (Input.GetMouseButtonDown(0) && !isPaused)
            {
                if (Cursor.lockState != CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        } 
        if (isPaused) {
            Cursor.lockState = CursorLockMode.None;
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
        player.transform.position = new Vector3(0, 3, 0);

        AudioManager.Instance.PlayMusic("PortalLevel");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene2");

        Reset();
        player.SetActive(false);
        // stop the music
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("MainMenu");

        isParallelDimension = false;
    }

    public void Restart()
    {
        isParallelDimension = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("HNScene");

        Reset();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Retro Music");
    }

    public void GameOver(bool isVictory)
    {
        isWon = isVictory;

        Reset();
        player.SetActive(false);
        // stop the music
        AudioManager.Instance.StopMusic();

        SceneManager.LoadScene("GameOver");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("HNScene");
        Reset();
        EnablePlayer();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Retro Music");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Reset()
    {
        // unload all the enemies
        ObjectPool.Instance.UnloadAllEnemies();
        // unload all the bullets
        ObjectPool.Instance.UnloadAllBullets();

        // reset player position
        player.transform.position = new Vector3(4.2f, 0, 3.6f);
        if (gun.GetComponent<Gun>().isEvolved)
            gun.GetComponent<Gun>().Devolve();

        player.GetComponent<PlayerController>().ResetHealth();
    }

    public void EnablePlayer()
    {
        player.SetActive(true);
    }

    public void DisablePlayer()
    {
        player.SetActive(false);
    }

}
