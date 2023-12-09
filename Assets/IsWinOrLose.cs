using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IsWinOrLose : MonoBehaviour
{
    private TextMeshProUGUI text;
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.StopMusic();       

        text = GetComponent<TextMeshProUGUI>();
        if (GameManager.Instance.isWon)
        {
            text.text = "You Win!";
            AudioManager.Instance.PlaySound("Victory", null);
        }
        else
        {
            text.text = "You Lose!";
            AudioManager.Instance.PlaySound("GameOver", null);
        }
    }
}
