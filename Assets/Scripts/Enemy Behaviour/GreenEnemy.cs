using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    public override void DoEnemyBehaviour()
    {
        if (shouldRun == false || hasJumped == true) return;

        if (CanJumpOnThePlayer() == true) JumpOnThePlayer();
        else RunTowardsTheEndZone();
    }

}
