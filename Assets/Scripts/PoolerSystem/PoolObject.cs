using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    WaveFormBullet,
    WaveFormBulletExplosion,
    EnemyDie,
    EnemyBullet
}

[System.Serializable]
public class PoolObject
{
    public PoolType type;
    public int amount;
    public GameObject prefab;
    [HideInInspector]
    public List<GameObject> gameObjects = new List<GameObject>();
}
