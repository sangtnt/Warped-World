using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class EnemyAction :Action 
{
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    public Player player;

    public override void OnAwake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public virtual void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetGameObjectByPoolType(PoolType.EnemyBullet);
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBullet>().SetDirection((player.transform.position - transform.position).normalized);
    }
}
