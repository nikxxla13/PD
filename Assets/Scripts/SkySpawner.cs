using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySpawner : MonoBehaviour
{
    public GameObject fallingRockPrefab;   // Assign your rock prefab here
    public float spawnInterval = 2f;       // How often to spawn rocks (seconds)

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRock();
            timer = 0f;
        }
    }

    void SpawnRock()
    {
        Instantiate(fallingRockPrefab, transform.position, Quaternion.identity);
    }

    // 🎨 Draws a downward line in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Adjust line length to match your level height
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 20f);
    }
}