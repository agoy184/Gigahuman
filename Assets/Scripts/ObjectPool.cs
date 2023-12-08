using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledBullets = new List<GameObject>();
    public List<GameObject> pooledEnemies = new List<GameObject>();
    public GameObject Bullet;
    public GameObject Enemy;
    private int bulletsToPool = 30;
    private int enemiesToPool = 20;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        InstantiateObjects(Bullet, pooledBullets, bulletsToPool);
        InstantiateObjects(Enemy, pooledEnemies, enemiesToPool);
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

    public void InstantiateBullets()
    {
        InstantiateObjects(Bullet, pooledBullets, bulletsToPool);
    }

    public void InstantiateEnemies()
    {
        InstantiateObjects(Enemy, pooledEnemies, enemiesToPool);
    }
}
