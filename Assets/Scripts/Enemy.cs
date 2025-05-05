using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    public float projectileSpeed = 5f;

    public Transform player; // Drag the player object here in the Inspector
    public float activationDistance = 8f; // How close the player must be for this enemy to shoot

    void Start()
    {
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (distanceToPlayer <= activationDistance)
                {
                    if (projectilePrefab != null)
                    {
                        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                        if (rb != null)
                        {
                            rb.velocity = new Vector2(-projectileSpeed, 0); // Still shoots to the left
                        }
                    }
                }
            }
        }
    }
}