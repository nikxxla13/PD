using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Assign in inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the score
            ScoreManager.instance.AddPoint();

            // Debug log before trying to spawn text
            Debug.Log("Player collected a coin! Attempting to spawn floating text...");

            // Spawn floating text above player
            if (floatingTextPrefab != null)
            {
                Vector3 spawnPos = new Vector3(0, 0, 0); // Center of the world
                GameObject textInstance = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity, GameObject.Find("FloatingTextCanvas").transform);
                Debug.Log("Floating text spawned at position: " + textInstance.transform.position);
            }
            else
            {
                Debug.LogWarning("No floating text prefab assigned!");
            }

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}