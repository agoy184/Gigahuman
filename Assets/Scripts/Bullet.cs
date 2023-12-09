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
            }
            
            if (other.gameObject.GetComponent<EnemyStatus>().hp > 0) {
                other.gameObject.GetComponent<EnemyNavMesh>().Stun(0.5f);
                other.gameObject.GetComponent<EnemyNavMesh>().Slow(1f, 0.5f);
            }
            gameObject.SetActive(false);
        } 
    }
    public void SetBulletType(int type)
    {
        bulletType = (BulletType)type;
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
