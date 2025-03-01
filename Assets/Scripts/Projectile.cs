using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;  
    private int hitCount = 0;  
    private bool isPlayerHit = false; 

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.TakeHit();  
                Destroy(gameObject);  
            }
        }
    }

    void OnDestroy()
    {
        Debug.Log("Projectile destroyed after hitting player");
    }
}
