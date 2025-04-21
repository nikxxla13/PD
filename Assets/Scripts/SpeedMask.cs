using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMask : Mask
{
    private float speedBoost = 1.5f;

    public override void ApplyEffect(Player player)
    {
        player.moveSpeed *= speedBoost;
    }

    public override void RemoveEffect(Player player)
    {
        player.moveSpeed /= speedBoost;
    }
}