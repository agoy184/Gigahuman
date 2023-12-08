using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    enum BulletType
    {
        Normal,
        Evolved
    }

    private BulletType bulletType;

    void OnEnable()
    {
        bulletType = BulletType.Normal;
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
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
            }
            
            if (other.gameObject.GetComponent<EnemyStatus>().hp > 0) {
                other.gameObject.GetComponent<EnemyNavMesh>().Stun(1f);
                other.gameObject.GetComponent<EnemyNavMesh>().Slow(2f, 0.2f);
            }
            gameObject.SetActive(false);
        }
    }
    public void SetBulletType(int type)
    {
        bulletType = (BulletType)type;
    }
}
