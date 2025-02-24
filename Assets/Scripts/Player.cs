using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed
    public float jumpForce = 10f; // Jump force
    private float moveInput;      // Input for horizontal movement
    private bool isGrounded;      // Check if the player is grounded
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component

    public Transform groundCheck; // A point to check if the player is grounded
    public LayerMask groundMask;  // Ground layer mask

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component attached to the player
    }

    void Update()
    {
        // Get horizontal input
        moveInput = Input.GetAxis("Horizontal");

        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);

        // Move the player
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
