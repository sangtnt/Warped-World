using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    public void DestroyBullet()
    {
        PoolManager.Instance.DeactivateGameObject(gameObject, PoolType.WaveFormBulletExplosion);
    }
    public void DestroyEnemy()
    {
        PoolManager.Instance.DeactivateGameObject(gameObject, PoolType.EnemyDie);
    }
}
