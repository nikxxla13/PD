using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Needed for UI Image
using TMPro;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Transform spriteTransform;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private float moveInput;
    private bool isGrounded;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform maskHolder;

    private GameObject currentMaskObject;
    public List<GameObject> maskPrefabs;

    private int currentMaskIndex = -1;

    public int maxHealth = 3; // Maximum health (adjustable in Inspector)
    [HideInInspector] public int currentHealth; // Current health, starts equal to maxHealth
    public List<Image> heartImages;  // List to hold the heart images
    public Sprite fullHeart;         // Sprite for a full heart
    public Sprite emptyHeart;        // Sprite for an empty heart

    private bool canJump = true;
    public float jumpCooldown = 1.0f;
    private int hitCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;  // Initialize current health
        UpdateHealthUI();  // Update the health UI (hearts)
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // SMOOTH FLIP
        if (moveInput > 0)
        {
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(1f, 1f, 1f), 0.1f);
        }
        else if (moveInput < 0)
        {
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(-1f, 1f, 1f), 0.1f);
        }

        // JUMPING
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            StartCoroutine(JumpCooldown());
        }

        // ANIMATOR UPDATES
        if (anim != null)
        {
            anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);
            anim.SetBool("isJumping", !isGrounded);
        }

        // MASK SWITCHING
        CheckForMaskSwitch();
    }

    public void TakeHit()
    {
        hitCounter++;
        Debug.Log("Player hit count: " + hitCounter);

        if (hitCounter >= 1)
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

        UpdateHealthUI();  // Update heart UI
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
    }

    // Update the heart images based on current health
    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].gameObject.SetActive(true);  // Enable the heart image
            }
            else
            {
                heartImages[i].gameObject.SetActive(false);  // Disable the heart image
            }
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
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    EnemyDeathController enemy = collision.gameObject.GetComponent<EnemyDeathController>();
                    if (enemy != null)
                    {
                        enemy.Die();
                        rb.velocity = new Vector2(rb.velocity.x, 8f); // Bounce!
                    }
                }
                else
                {
                    TakeHit();
                }
            }
        }
    }
}
