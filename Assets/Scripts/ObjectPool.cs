using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public List<GameObject> pooledBullets = new List<GameObject>();
    public List<GameObject> pooledEnemies = new List<GameObject>();
    public GameObject Bullet;
    public GameObject[] Enemies;
    private int bulletsToPool = 30;
    private int enemiesToPool = 10;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InstantiateBullets();
        InstantiateEnemies();
    }

    public void InstantiateObjects(GameObject objectToPool, List<GameObject> objects, int amount)
    {
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(objectToPool);
            DontDestroyOnLoad(tmp);
            tmp.SetActive(false);
            objects.Add(tmp);
        }
    }

    public GameObject GetPooledObject(List<GameObject> pooledObjects)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public GameObject GetPooledBullet()
    {
        return GetPooledObject(pooledBullets);
    }

    public GameObject GetPooledEnemy()
    {
        return GetPooledObject(pooledEnemies);
    }

    public GameObject GetRandomPooledEnemy()
    {
        if (pooledEnemies.TrueForAll(enemy => enemy.activeInHierarchy)) {
            return null;
        }

        GameObject randomEnemy = pooledEnemies[Random.Range(0, pooledEnemies.Count)];
        if (randomEnemy.activeInHierarchy) {
            return GetRandomPooledEnemy();
        }
        else {
            return randomEnemy;
        }
    }

    public void InstantiateBullets()
    {
        InstantiateObjects(Bullet, pooledBullets, bulletsToPool);
    }

    public void InstantiateEnemies()
    {
        foreach (GameObject enemy in Enemies)
        {
            InstantiateObjects(enemy, pooledEnemies, enemiesToPool);
        }
    }

    public void UnloadAllBullets()
    {
        foreach (GameObject bullet in pooledBullets)
        {
            bullet.SetActive(false);
        }
    }

    public void UnloadAllEnemies()
    {
        foreach (GameObject enemy in pooledEnemies)
        {
            enemy.SetActive(false);
        }
    }
}
