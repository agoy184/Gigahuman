using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float bulletSpeed = 80f;
    private float fireRate = 0.5f;
    private float fireCooldown = 0f;

    private void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        } 
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        fireCooldown = fireRate;
    }

    public bool CanFire()
    {
        return fireCooldown <= 0;
    }

    public void Evolve()
    {
        bulletSpeed = 100f;
        fireRate = 0.25f;
    }

    public void Devolve()
    {
        bulletSpeed = 80f;
        fireRate = 0.5f;
    }
}
