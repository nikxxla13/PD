using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Unique ID for this collectible (e.g., 'lvl2_coin_1')")]
    public string collectibleID;

    public GameObject floatingTextPrefab; // Assign in inspector
    public Canvas floatingTextCanvas;     // Assign your screen-space canvas

    void Start()
    {
        // Check if this collectible was already collected
        if (CheckpointMemorySystem.instance != null && 
            CheckpointMemorySystem.instance.HasCollected(collectibleID))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddPoint();

            if (floatingTextPrefab != null && floatingTextCanvas != null)
            {
                Vector3 worldPos = transform.position + new Vector3(0, 1f, 0); // above coin
                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

                GameObject textInstance = Instantiate(floatingTextPrefab, floatingTextCanvas.transform);
                textInstance.GetComponent<RectTransform>().position = screenPos;
            }

            // Mark as collected
            if (CheckpointMemorySystem.instance != null)
            {
                CheckpointMemorySystem.instance.MarkCollected(collectibleID);
            }

            Destroy(gameObject);
        }
    }
}