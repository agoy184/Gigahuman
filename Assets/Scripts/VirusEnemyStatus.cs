using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusEnemyStatus : EnemyStatus
{
    public NavMeshAgent navMesh;
    [SerializeField] private HealthBar healthBar;
    public int maxHp = 100;
    private void Start()
    {
        enemyType = EnemyType.Virus;
        rend = gameObject.GetComponent<Renderer>();
        defaultColor = rend.material.color;

        navMesh = gameObject.GetComponent<NavMeshAgent>();

        navMesh.speed = speed * 1.25f;

        hp = 100;
    }

    public override void TakeDamage(int damage)
    {
        hp -= damage;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.2f);
        healthBar.UpdateHealthBar(maxHp, hp);
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
