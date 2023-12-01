using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnEnable()
    {
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
            other.gameObject.GetComponent<EnemyStatus>().TakeDamage(10);
            if (other.gameObject.GetComponent<EnemyStatus>().hp > 0) {
                other.gameObject.GetComponent<EnemyNavMesh>().Stun(1f);
                other.gameObject.GetComponent<EnemyNavMesh>().Slow(2f, 0.2f);
            }
            gameObject.SetActive(false);
        }
    }
}
