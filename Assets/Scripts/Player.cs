using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public int maxHealth = 3;
    [HideInInspector] public int currentHealth;
    public List<Image> heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool canJump = true;
    public float jumpCooldown = 1.0f;
    private int hitCounter = 0;

    // === Coyote Time Variables ===
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    IEnumerator Start()
    {
        Debug.Log("Animator assigned: " + (anim != null));
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        UpdateHealthUI();

        yield return null; // Let scene load finish before anything important happens
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // COYOTE TIME UPDATE
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // SMOOTH FLIP
        if (moveInput > 0)
        {
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(1f, 1f, 1f), 0.1f);
        }
        else if (moveInput < 0)
        {
            spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, new Vector3(-1f, 1f, 1f), 0.1f);
        }

        // JUMPING (WITH COYOTE TIME)
        if (coyoteTimeCounter > 0f && Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
            coyoteTimeCounter = 0f;
            StartCoroutine(JumpCooldown());
        }

        if (anim != null)
        {
            anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);
            anim.SetBool("isJumping", !isGrounded);
        }

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

        UpdateHealthUI();
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            heartImages[i].gameObject.SetActive(i < currentHealth);
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

        currentMaskIndex = index;
        Debug.Log("Equipped Mask: " + newMaskPrefab.name);
    }

    void ToggleMask(int index)
    {
        if (currentMaskObject != null && currentMaskIndex == index)
        {
            Mask currentMask = currentMaskObject.GetComponent<Mask>();
            if (currentMask != null)
            {
                currentMask.RemoveEffect(this);
            }

            Destroy(currentMaskObject);
            currentMaskObject = null;
            currentMaskIndex = -1;

            Debug.Log("Mask unequipped – back to original form!");
        }
        else
        {
            EquipMask(index);
            currentMaskIndex = index;
        }
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
                        rb.velocity = new Vector2(rb.velocity.x, 8f);
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
