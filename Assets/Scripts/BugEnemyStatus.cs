using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugEnemyStatus : EnemyStatus
{
    public NavMeshAgent navMesh;
    private void Start()
    {
        enemyType = EnemyType.Bug;
        navMesh = gameObject.GetComponent<NavMeshAgent>(); 

        navMesh.speed = speed * 1.5f;
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
