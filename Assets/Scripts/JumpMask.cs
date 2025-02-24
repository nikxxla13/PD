using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMask : Mask
{
    private float jumpBoost = 1.5f;

    public override void ApplyEffect(Player player)
    {
        player.jumpForce *= jumpBoost;
        player.GetComponent<SpriteRenderer>().color = Color.blue; 
    }

    public override void RemoveEffect(Player player)
    {
        player.jumpForce /= jumpBoost;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
