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
        rend = gameObject.GetComponent<Renderer>();
        defaultColor = rend.material.color;
        navMesh = gameObject.GetComponent<NavMeshAgent>(); 

        navMesh.speed = speed * 1.5f;

        hp = 50;
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.2f);
        if (hp <= 0)
        {
            Ragdoll();
            Invoke("Die", 2f);
        }
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
        AudioManager.Instance.PlaySound("Explosion");
        // Gradually shrink the enemy
        StartCoroutine(Shrink());
    }
}
