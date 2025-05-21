using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingProjectile : MonoBehaviour
{
    public float fallSpeed = 5f;
    public int damage = 1;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeHit();
            }

            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Falling projectile destroyed");
    }

    // 🎨 Shows a downward line in Scene view to help with placement
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw a line downwards for reference (e.g., 10 units)
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 10f);
    }
}