using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    private Color defaultColor;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.2f);
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void ResetColor() {
        rend.material.color = defaultColor;
    }

    // on collision with player, deal damage
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10, 1f);
        }
    }
}
