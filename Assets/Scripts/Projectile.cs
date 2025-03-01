using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;  
    private int hitCount = 0;  

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            hitCount++;

            if (hitCount >= 3)
            {
                player.TakeDamage();
                hitCount = 0; 
            }

            Destroy(gameObject); 
        }
    }
}
}

