using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 10f; 
    private float moveInput;     
    private bool isGrounded;      
    private Rigidbody2D rb;       

    public Transform groundCheck; 
    public LayerMask groundMask;  

    private Mask currentMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        // Ground check with debugging
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
        Debug.Log("Is Grounded: " + isGrounded);

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("JumpForce Before Jump: " + jumpForce); // Debugging
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    
    public void EquipMask(Mask newMask)
    {
        if (currentMask != null)
        {
            currentMask.RemoveEffect(this);
        }

        currentMask = newMask;
        currentMask.ApplyEffect(this);
        Debug.Log("Equipped Mask: " + newMask.name);
    }
}
