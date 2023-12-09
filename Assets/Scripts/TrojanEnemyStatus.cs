using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanEnemyStatus : EnemyStatus
{
    private void Start()
    {
        enemyType = EnemyType.Trojan;
    }

    // on collision with player, deal damage
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(10, 1f);
        }
    }

    public override void Ragdoll() {
        AudioManager.Instance.PlaySound("Explosion", audioSource);
        // Gradually shrink the enemy
        StartCoroutine(Shrink());
    }
}
