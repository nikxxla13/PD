using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMask : Mask
{
    private Vector3 smallSize = new Vector3(0.5f, 0.5f, 1);

    public override void ApplyEffect(Player player)
    {
        player.transform.localScale = smallSize;
        player.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public override void RemoveEffect(Player player)
    {
        player.transform.localScale = Vector3.one;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
