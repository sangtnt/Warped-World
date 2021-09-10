using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    public float damage;
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    AudioClip explosionClip;

    private Vector2 _dir;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Set flying direction for bullet
    public void SetDirection(Vector2 dir)
    {
        this._dir = dir;
        float rotation = GetRotation();
        Quaternion deg = Quaternion.AngleAxis(rotation, Vector3.forward);
        transform.rotation = deg;
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = _dir * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            HideObject();
            Explosion();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            HideObject();
        }
    }
    private float GetRotation()
    {
        float rotation = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        return rotation;
    }
    void Explosion()
    {
        GameObject explosion = PoolManager.Instance.GetGameObjectByPoolType(PoolType.WaveFormBulletExplosion);
        explosion.transform.position = gameObject.transform.position;
        AudioManager.Instance.PlayClipOneShot(explosionClip);
    }
    void HideObject()
    {
        PoolManager.Instance.DeactivateGameObject(gameObject, PoolType.EnemyBullet);
    }
}
