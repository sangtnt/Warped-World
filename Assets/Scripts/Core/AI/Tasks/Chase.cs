using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyAction
{
    private Path path;
    private Seeker seeker;
    private int currWaypoint;
    public float moveSpeed = 100;
    public float distanceToNextPoint = 3;

    public override void OnAwake()
    {
        base.OnAwake();
        seeker = GetComponent<Seeker>();
    }
    public override void OnFixedUpdate()
    {
        GeneratePath();
    }
    public void GeneratePath()
    {
        seeker.StartPath(transform.position, player.transform.position, UpdatePath);
    }
    public void UpdatePath(Path path)
    {
        this.path = path;
        currWaypoint = 0;
    }
    public void FindPathAndMove()
    {
        if(currWaypoint>= path.vectorPath.Count)
        {
            return;
        }
        Vector2 vectorMove = (path.vectorPath[currWaypoint] - transform.position).normalized;
        rigidbody2D.AddForce(vectorMove * moveSpeed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, path.vectorPath[currWaypoint]);
        if (distance < distanceToNextPoint)
        {
            currWaypoint++;
        }
    }
    public override TaskStatus OnUpdate()
    {
        FindPathAndMove();
        return TaskStatus.Running;
    }
}
