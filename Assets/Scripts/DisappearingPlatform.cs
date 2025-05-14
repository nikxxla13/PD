using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 1f;     // Time after touch before vanishing starts
    public float fallDistance = 0.2f;     // How far the platform visually drops
    public float fadeDuration = 0.5f;     // Time over which it fades and falls

    private Collider2D col;
    private SpriteRenderer sr;
    private bool hasDisappeared = false;
    private Vector3 originalPosition;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;

        hasDisappeared = false;
        col.enabled = true;
        if (sr != null)
        {
            sr.enabled = true;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasDisappeared && collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(StartDisappearing), disappearDelay);
        }
    }

    void StartDisappearing()
    {
        hasDisappeared = true;
        StartCoroutine(FallAndFadeTogether());
    }

    IEnumerator FallAndFadeTogether()
    {
        float elapsed = 0f;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * fallDistance;
        Color startColor = sr.color;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;

            // Move down while fading
            transform.position = Vector3.Lerp(start, end, t);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, t));

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final state
        transform.position = end;
        sr.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        col.enabled = false;
        sr.enabled = false;
    }
}