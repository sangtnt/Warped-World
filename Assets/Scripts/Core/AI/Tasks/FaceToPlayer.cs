using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToPlayer : EnemyAction
{
    public override void OnStart()
    {
    }
    private void Flip(Vector2 facing)
    {
        if (facing.x < 0)
        {
            transform.localScale = Vector2.one;
        }
        else if (facing.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    public override TaskStatus OnUpdate()
    {
        Vector2 facing = (player.transform.position - transform.position).normalized;
        Flip(facing);
        return TaskStatus.Success;
    }
}
