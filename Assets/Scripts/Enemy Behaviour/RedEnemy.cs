using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{

    void Start()
    {
        pathfinding.AgentSpeed = forwardRunSpeed;
    }

    public override void DoEnemyBehaviour()
    {
        if (shouldRun == false || hasJumped == true) return;

        if (CanJumpOnThePlayer() == true) JumpOnThePlayer();
        else pathfinding.HandlePathfinding();
    }

}
