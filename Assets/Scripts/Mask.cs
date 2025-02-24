using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mask : MonoBehaviour
{
    public Sprite maskSprite; 

    public abstract void ApplyEffect(Player player);
    public abstract void RemoveEffect(Player player);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.EquipMask(this);
        }
    }
}

