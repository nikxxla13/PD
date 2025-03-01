using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectilePrefab;  
    public Transform firePoint; 
    public float shootInterval = 2f;  
    public float projectileSpeed = 5f;

    void Start()
    {
        StartCoroutine(ShootProjectiles());
    }

    IEnumerator ShootProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = new Vector2(-projectileSpeed, 0);
                }
            }
        }
    }
}
