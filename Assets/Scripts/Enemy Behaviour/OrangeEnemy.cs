using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeEnemy : Enemy
{
    [SerializeField] bool isOnMainPath = false;
    [SerializeField] float agentSpeed = 4f;

    void Start()
    {
        pathfinding.AgentSpeed = agentSpeed;
    }

    public override void DoEnemyBehaviour()
    {
        if (shouldRun == false || hasJumped == true) return;
        if (CanJumpOnThePlayer() == true) JumpOnThePlayer();
        else if (IsOnMainPath == false) pathfinding.HandlePathfinding();
        else
        {
            pathfinding.TurnOffNavMeshAgent();
            RunTowardsTheEndZone();
        }
    }

    public bool IsOnMainPath
    {
        get { return isOnMainPath; }
        set
        {
            isOnMainPath = value;
            pathfinding.TurnOffNavMeshAgent();
        }
    }

}
