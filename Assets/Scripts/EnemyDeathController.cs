using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{
    public GameObject visuals;
    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        if (visuals != null)
        {
            anim = visuals.GetComponent<Animator>();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        // Disable collider so player doesn't bounce again
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Disable enemy movement script if needed
        // Replace "EnemyPatrol" with your movement script name if different
        var movementScript = GetComponent<MonoBehaviour>();
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        Destroy(gameObject, 0.5f);
    }
}