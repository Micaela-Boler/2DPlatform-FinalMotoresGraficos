using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUp
{
    protected override void ApplyPowerUp()
    {
        player.GetComponent<MovePlayer>().availableJumps += 2;
    }
}
