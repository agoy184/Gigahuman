using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    enum BulletType
    {
        Normal,
        Evolved,
        ChargeShot,

        EvolvedChargeShot
    }

    private BulletType bulletType;

    float chargeTime = 0f;

    void OnEnable()
    {
        transform.localScale = Vector3.one * 0.5f;
        bulletType = BulletType.Normal;
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(1f);
        if (gameObject.activeSelf)
        {
            StartCoroutine(Shrink());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            switch (bulletType)
            {
                case BulletType.Normal:
                    other.gameObject.GetComponent<EnemyStatus>().TakeDamage(25);
                    break;
                case BulletType.Evolved:
                    other.gameObject.GetComponent<EnemyStatus>().TakeDamage(50);
                    break;
                case BulletType.ChargeShot:
                    other.gameObject.GetComponent<EnemyStatus>().TakeDamage((int) (50*chargeTime));
                    break;
                case BulletType.EvolvedChargeShot:
                    other.gameObject.GetComponent<EnemyStatus>().TakeDamage((int) (100*chargeTime));
                    break;
            }
            
            if (other.gameObject.GetComponent<EnemyStatus>().hp > 0) {
                other.gameObject.GetComponent<EnemyNavMesh>().Stun(0.5f);
                other.gameObject.GetComponent<EnemyNavMesh>().Slow(1f, 0.5f);
            }
            if (bulletType != BulletType.ChargeShot && bulletType != BulletType.EvolvedChargeShot) gameObject.SetActive(false);
        } 
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (bulletType == BulletType.EvolvedChargeShot) {
                other.gameObject.GetComponent<EnemyStatus>().TakeDamage((int) (100*chargeTime));
                if (other.gameObject.GetComponent<EnemyStatus>().hp > 0) {
                    other.gameObject.GetComponent<EnemyNavMesh>().Stun(1f);
                    other.gameObject.GetComponent<EnemyNavMesh>().Slow(1f, 0.5f);
                }
            }
        }
    }


    public void SetBulletType(int type)
    {
        bulletType = (BulletType)type;
    }

    public void SetChargeTime(float time)
    {
        chargeTime = time;
        if (bulletType == BulletType.ChargeShot)
        { 
            transform.localScale = Vector3.one * (0.5f + (chargeTime / 2f));
        } else {
            transform.localScale = Vector3.one * (0.5f + chargeTime);
        }
        // scale mass with size
        GetComponent<Rigidbody>().mass = 1f + (chargeTime * 2f);
    }

    public IEnumerator Shrink()
    {
        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, time);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
