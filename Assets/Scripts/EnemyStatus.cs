using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatus : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    public Color defaultColor;
    public Renderer rend;

    [SerializeField] public HealthBar healthBar;

    public AudioSource audioSource;

    public float speed = 3.5f;

    public enum EnemyType
    {
        Trojan,
        Bug,
        Virus
    }

    void OnEnable()
    {
        rend = gameObject.GetComponent<Renderer>();
        defaultColor = rend.material.color;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public EnemyType enemyType;

    public void TakeDamage(int damage) {
        AudioManager.Instance.PlaySound("EnemyHurt", audioSource);
        hp -= damage;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.2f);
        healthBar.UpdateHealthBar(maxHp, hp);
        if (hp <= 0)
        {
            Ragdoll();
            Invoke("Die", 2f);
        }
    }

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
