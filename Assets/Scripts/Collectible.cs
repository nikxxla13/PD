using UnityEngine;

using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Assign in inspector
    public Canvas floatingTextCanvas;     // Assign your screen-space canvas

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

            Destroy(gameObject);
        }
    }
}
