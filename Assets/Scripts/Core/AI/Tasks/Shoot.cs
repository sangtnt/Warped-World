using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shoot : EnemyAction
{
    public float shootingTime;
    bool canShoot;
    Tween buildUp;
    public override void OnStart()
    {
        DOVirtual.DelayedCall(shootingTime, StartShoot, false);
    }
    private void StartShoot()
    {
        canShoot = true;
        Shoot();
    }
    public override TaskStatus OnUpdate()
    {
        return canShoot ? TaskStatus.Success : TaskStatus.Running;
    }
    public override void OnEnd()
    {
        buildUp?.Kill();
        canShoot = false;
    }
}
