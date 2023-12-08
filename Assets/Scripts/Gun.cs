using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float bulletSpeed = 80f;
    private float fireRate = 0.5f;
    private float fireCooldown = 0f;
    private MeshRenderer meshRenderer;

    private bool isEvolved = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        string animationName = isEvolved ? "EvolvedFire" : "Fire";
        animator.SetTrigger(animationName);
        GameObject bullet = ObjectPool.SharedInstance.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        } 
        bullet.GetComponent<Bullet>().SetBulletType(isEvolved ? 1 : 0);
        bullet.GetComponent<Rigidbody>().velocity = GameManager.Instance.GetPlayer().transform.forward * bulletSpeed;

        fireCooldown = fireRate;
    }

    public bool CanFire()
    {
        return fireCooldown <= 0;
    }

    public void Evolve()
    {
        animator.SetTrigger("Evolve");

        meshRenderer.material.color = Color.red;

        bulletSpeed = 150f;
        fireRate = 0.1f;
        isEvolved = true;
    }

    public void Devolve()
    {
        meshRenderer.material.color = Color.yellow;

        bulletSpeed = 80f;
        fireRate = 0.5f;
        isEvolved = false;
    }
}
