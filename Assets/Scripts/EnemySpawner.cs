using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform portalTransform;
    public float spawnInterval = 2f;
    public float riseSpeed = 2f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Calculate the spawn position below the portal
        Vector3 spawnPosition = portalTransform.position - Vector3.up * 2f;

        // Instantiate the enemy at the calculated position
        GameObject newEnemy = ObjectPool.SharedInstance.GetPooledEnemy();
        if (newEnemy != null)
        {
            newEnemy.transform.position = spawnPosition;
            newEnemy.SetActive(true);
            
            // Disable the NavMeshAgent initially
            NavMeshAgent navMeshAgent = newEnemy.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }

            // Move the enemy towards the player over time
            StartCoroutine(RiseToSurface(newEnemy.transform, navMeshAgent));
        }
    }

    IEnumerator RiseToSurface(Transform enemyTransform, NavMeshAgent navMeshAgent)
    {
        // Move the enemy towards the surface
        while (enemyTransform.position.y < GameManager.Instance.GetPlayer().transform.position.y)
        {
            float step = riseSpeed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(enemyTransform.position.x, GameManager.Instance.GetPlayer().transform.position.y, enemyTransform.position.z);
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, step);
            yield return null;
        }

        // Re-enable the NavMeshAgent once the enemy reaches the surface
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }

    
    }
}