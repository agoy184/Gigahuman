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
        AudioManager.Instance.PlaySound("Shoot");
        string animationName = isEvolved ? "EvolvedFire" : "Fire";
        animator.SetTrigger(animationName);
        GameObject bullet = ObjectPool.SharedInstance.GetPooledBullet();
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
}
