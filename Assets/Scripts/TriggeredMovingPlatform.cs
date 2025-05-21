using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveToTarget : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 2f;

    private bool moveToTarget = false;

    void Update()
    {
        if (moveToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
            {
                moveToTarget = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            moveToTarget = true;
            other.transform.SetParent(transform); // Stick player to platform
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Unstick player when they leave
        }
    }

    // 🎨 Draws a line in Scene view to visualize the target path
    private void OnDrawGizmos()
    {
        if (targetPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, targetPoint.position);
            Gizmos.DrawSphere(targetPoint.position, 0.2f); // Optional: draws a small dot at the destination
        }
    }
}