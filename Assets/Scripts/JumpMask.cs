using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMask : Mask
{
    private float jumpBoost = 1.5f;
    private float jumpCooldown = 0.5f;  // Cooldown time 
    private bool canJump = true; 

    public override void ApplyEffect(Player player)
    {
        player.jumpForce *= jumpBoost;
        player.GetComponent<SpriteRenderer>().color = Color.blue; 

        // Start the cooldown 
        player.StartCoroutine(JumpCooldown(player));
    }

    public override void RemoveEffect(Player player)
    {
        player.jumpForce /= jumpBoost;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator JumpCooldown(Player player)
    {
        canJump = false;  
        yield return new WaitForSeconds(jumpCooldown);  
        canJump = true;  
    }
}
