using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform portalTransform;
    public float spawnInterval = 2f;
    private float riseSpeed = 3f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        AudioManager.Instance.PlaySound("PortalSpawnEnemy", audioSource);
        // Calculate the spawn position below the portal
        Vector3 spawnPosition = portalTransform.position - Vector3.up * 2f;

        // Instantiate the enemy at the calculated position
        GameObject newEnemy = ObjectPool.Instance.GetRandomPooledEnemy();
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
        float destinationY = GameManager.Instance.GetPlayer().transform.position.y;
        // disable gravity for the enemy 
        // disable collider for the enemy
        enemyTransform.GetComponent<Rigidbody>().useGravity = false;
        enemyTransform.GetComponent<Collider>().enabled = false;

        // Move the enemy towards the surface
        while (enemyTransform.position.y < destinationY)
        {
            float step = riseSpeed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(enemyTransform.position.x, destinationY, enemyTransform.position.z);
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, step);
            yield return null;
        }

        // Re-enable the NavMeshAgent once the enemy reaches the surface
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }

        // Re-enable gravity for the enemy
        // Re-enable collider for the enemy
        enemyTransform.GetComponent<Rigidbody>().useGravity = true;
        enemyTransform.GetComponent<Collider>().enabled = true;
    }
}