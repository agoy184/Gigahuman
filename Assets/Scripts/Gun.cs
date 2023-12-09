using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float bulletSpeed = 80f;
    private float fireRate = 0.5f;
    private float fireCooldown = 0f;

    public bool isCharging = false;

    private float chargeTime = 0f;
    private float maximumChargeTime = 7f;
    private MeshRenderer meshRenderer;

    public bool isEvolved = false;

    private Animator animator;

    private AudioSource audioSource;

    public Camera cam;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            AudioManager.Instance.PlaySound("Charge", audioSource);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Charge();
        } else {
            // if charge sound is playing, stop it
            if (AudioManager.Instance.IsSoundPlaying("Charge", audioSource))
                audioSource.Stop();

            if (chargeTime >= 1f) {
                ChargeShot();
            }
            chargeTime = 0f;
            isCharging = false;
        }
    }

    public void Fire()
    {
        AudioManager.Instance.PlaySound("Shoot", audioSource);
        string animationName = isEvolved ? "EvolvedFire" : "Fire";
        animator.SetTrigger(animationName);
        GameObject bullet = ObjectPool.Instance.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetBulletType(isEvolved ? 1 : 0);
            // fire the bullet in the direction of the camera
            bullet.GetComponent<Rigidbody>().velocity = cam.transform.TransformDirection(Vector3.forward * bulletSpeed);
        } 

        fireCooldown = fireRate;
    }

    public void Charge()
    {
        isCharging = true;
        animator.SetTrigger("Evolve");

        if (chargeTime < maximumChargeTime) {
            chargeTime += Time.deltaTime;
        }
    }

    public void ChargeShot()
    {
        AudioManager.Instance.PlaySound("ChargeShot", audioSource);
        animator.SetTrigger("ChargeShot");
        GameObject bullet = ObjectPool.Instance.GetPooledBullet();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetBulletType(isEvolved ? 3 : 2);
            bullet.GetComponent<Bullet>().SetChargeTime(chargeTime);
            if (isEvolved) {
                bullet.GetComponent<Collider>().isTrigger = true;
            }
            // fire the bullet in the direction of the camera
            bullet.GetComponent<Rigidbody>().velocity = cam.transform.TransformDirection(Vector3.forward * bulletSpeed);
        } 

        fireCooldown = fireRate;
    }

    public bool CanFire()
    {
        return fireCooldown <= 0;
    }

    public void Evolve()
    {
        animator.SetTrigger("Evolve");
        AudioManager.Instance.PlaySound("GunEvolve", audioSource);

        meshRenderer.material.color = Color.red;

        bulletSpeed = 100f;
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
