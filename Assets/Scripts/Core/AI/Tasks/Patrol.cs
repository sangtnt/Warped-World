using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyConditional
{
    public float attackDistance;
    public override TaskStatus OnUpdate()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > attackDistance)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
