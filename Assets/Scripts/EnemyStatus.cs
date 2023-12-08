using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatus : MonoBehaviour
{
    public int hp = 100;
    public Color defaultColor;
    public Renderer rend;

    public float speed = 3.5f;

    public enum EnemyType
    {
        Trojan,
        Bug,
        Virus
    }

    public EnemyType enemyType;

    public abstract void TakeDamage(int damage);

    public abstract void Ragdoll();

    void ResetColor() {
        rend.material.color = defaultColor;
    }
    void Die()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator Shrink() {
        float time = 0;
        while (time < 1f) {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, time);
            yield return null;
        }
    }

    public abstract void OnCollisionEnter(Collision collision);
}
