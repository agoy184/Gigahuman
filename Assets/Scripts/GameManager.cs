using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        // Insert Pause/Resume code here, for whoever's doing menus

        // Zoom in/out
        if (Input.GetKey(KeyCode.Equals))
        {
            TopDownCamera.Instance.SetHeight(TopDownCamera.Instance.GetHeight() - 0.1f);
        }
        else if (Input.GetKey(KeyCode.Minus))
        {
            TopDownCamera.Instance.SetHeight(TopDownCamera.Instance.GetHeight() + 0.1f);
        }
    }

    public GameObject GetPlayer()
    {
        Debug.Log("Player: " + player);
        return player;
    }

}
