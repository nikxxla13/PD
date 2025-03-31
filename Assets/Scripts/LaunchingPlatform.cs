using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Platform that launches the player upwards when touched
public class LaunchingPlatform : MonoBehaviour {
    public float launchForce = 10f; // The force with which the player will be launched

    void OnCollisionEnter2D(Collision2D collision) {
        // Check if the player is the one colliding with the platform
        if (collision.gameObject.CompareTag("Player")) {
            // Get the Rigidbody2D component of the player
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null) {
                // Apply an upward force to the player's Rigidbody2D to launch them up
                rb.velocity = new Vector2(rb.velocity.x, launchForce);
            }
        }
    }
}