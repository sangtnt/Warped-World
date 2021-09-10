using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class EnemyConditional : Conditional
{
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    public Player player;

    public override void OnAwake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
}
