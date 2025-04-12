using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Transform spriteTransform; // NEW: for smooth flipping

    public float moveSpeed = 5f;  
    public float jumpForce = 10f; 
    private float moveInput;     
    private bool isGrounded;      
    private Rigidbody2D rb;    

    public Transform groundCheck; 
    public LayerMask groundMask;  
    public Transform maskHolder;

    private GameObject currentMaskObject; 
    public List<GameObject> maskPrefabs;  // All available masks

    private int currentMaskIndex = -1;

    public int maxHealth = 3;
    private int currentHealth;
    public TextMeshProUGUI healthText;

    private bool canJump = true; 
    public float jumpCooldown = 1.0f; // Adjusted to 1 second
    private int hitCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // SMOOTH FLIP (using Lerp)
        if (moveInput > 0)
        {
            // Smooth transition from left to right
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(1f, 1f, 1f), 0.1f);
        }
        else if (moveInput < 0)
        {
            // Smooth transition from right to left
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(-1f, 1f, 1f), 0.1f);
        }

        // JUMPING
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false; 
            StartCoroutine(JumpCooldown()); // Start the cooldown when jumping
        }

        // ANIMATOR UPDATES
        if (anim != null)
        {
            anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);
            anim.SetBool("isJumping", !isGrounded); // Jumping condition based on isGrounded
        }

        // MASK SWITCHING
        CheckForMaskSwitch();
    }

    public void TakeHit()  
    {
        hitCounter++;  
        Debug.Log("Player hit count: " + hitCounter);

        if (hitCounter >= 3)  
        {
            TakeDamage();
            hitCounter = 0; 
        }
    }

    public void TakeDamage()
    {
        currentHealth--;  
        Debug.Log("Current Health: " + currentHealth); 

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player Died!");
            Die();
        }

        UpdateHealthUI();
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    void CheckForMaskSwitch()
    {
        if (Input.GetKeyDown(KeyCode.F) && maskPrefabs.Count > 0)
        {
            EquipMask(0);
        }
        else if (Input.GetKeyDown(KeyCode.G) && maskPrefabs.Count > 1)
        {
            EquipMask(1);
        }
        else if (Input.GetKeyDown(KeyCode.H) && maskPrefabs.Count > 2)
        {
            EquipMask(2);
        }
    }

    public void EquipMask(int index)
    {
        if (index < 0 || index >= maskPrefabs.Count) return;

        if (currentMaskObject != null)
        {
            Mask previousMask = currentMaskObject.GetComponent<Mask>();
            if (previousMask != null)
            {
                previousMask.RemoveEffect(this);
            }

            Destroy(currentMaskObject);
        }

        GameObject newMaskPrefab = maskPrefabs[index];
        GameObject newMaskObject = Instantiate(newMaskPrefab, maskHolder.position, Quaternion.identity);
        newMaskObject.transform.SetParent(maskHolder);
        currentMaskObject = newMaskObject;

        Mask newMask = newMaskObject.GetComponent<Mask>();
        if (newMask != null)
        {
            newMask.ApplyEffect(this);
        }

        Debug.Log("Equipped Mask: " + newMaskPrefab.name);
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown); // Wait for the cooldown
        canJump = true;  // Re-enable jumping after the cooldown
    }
}
