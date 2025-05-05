using UnityEngine;
using TMPro;
using System.Collections;


public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;
    private TextMeshProUGUI text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
        StartCoroutine(FloatAndFade());
    }

    private IEnumerator FloatAndFade()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 0.5f, 0); // move up a bit

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;

            // Move upward
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Fade out
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}