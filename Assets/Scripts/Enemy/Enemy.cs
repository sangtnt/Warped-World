using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health;
    [SerializeField]
    float damage;
    protected Rigidbody2D rb;
    [SerializeField]
    AudioClip DeathClip;
    [SerializeField]
    AudioClip takeDamageClip;
    private void Reset()
    {
        gameObject.tag = "Enemy";
    }
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void TakeDamage(float damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            Die();
            return;
        }
        AudioManager.Instance.PlayClipOneShot(takeDamageClip);
    }
    public virtual float GetDamage()
    {
        return damage;
    }
    public virtual float GetHealth()
    {
        return health;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void Die()
    {
        Debug.Log("attack");
        gameObject.SetActive(false);
        GameObject explosion = PoolManager.Instance.GetGameObjectByPoolType(PoolType.EnemyDie);
        explosion.transform.position = gameObject.transform.position;
        AudioManager.Instance.PlayClipOneShot(DeathClip);
    }
}
