using UnityEngine;

public class EyeEnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    private int currentPointIndex = 0;
    private Animator animator;
    private Transform spriteTransform;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
    }

    private void Update()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 direction = targetPoint.position - transform.position;

        // Move towards the point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Set walking animation
        bool isWalking = direction.magnitude > 0.05f;
        animator.SetBool("isWalking", isWalking);

        // Flip sprite based on direction
        if (direction.x > 0.1f)
            spriteTransform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.1f)
            spriteTransform.localScale = new Vector3(-1, 1, 1);

        // Switch to next point if close enough
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}