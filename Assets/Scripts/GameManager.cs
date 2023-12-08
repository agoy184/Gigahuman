using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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

        AudioManager.Instance.PlayMusic("Retro Music");
    }

    private void Update()
    {
        // Insert Pause/Resume code here, for whoever's doing menus
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

}
