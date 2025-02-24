using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI management

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 10f; 
    private float moveInput;     
    private bool isGrounded;      
    private Rigidbody2D rb;       

    public Transform groundCheck; 
    public LayerMask groundMask;  
    public Transform maskHolder;

    private Mask currentMask;
    private GameObject currentMaskObject;

    public List<Mask> collectedMasks = new List<Mask>(); // List to store collected masks
    public Image maskUIImage;  // UI Image to show current mask
    public List<Button> maskButtons = new List<Button>(); // List of UI buttons for switching masks

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  

        // Initialize buttons to switch masks
        foreach (Button button in maskButtons)
        {
            button.onClick.AddListener(() => EquipMaskFromButton(button)); // Equip mask when button is clicked
        }
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
        Debug.Log("Is Grounded: " + isGrounded);

        // Handle player movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("JumpForce Before Jump: " + jumpForce);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Equip the mask when collected
    public void EquipMask(Mask newMask)
    {
        // Remove the current mask's effect
        if (currentMask != null)
        {
            currentMask.RemoveEffect(this);
        }

        // Apply new mask's effect
        currentMask = newMask;
        currentMask.ApplyEffect(this);
        Debug.Log("Equipped Mask: " + newMask.name);

        // Create the mask object and attach it to the mask holder
        currentMaskObject = Instantiate(newMask.maskPrefab, maskHolder.position, Quaternion.identity);
        currentMaskObject.transform.SetParent(maskHolder);

        // Show the mask on the player (can include sprite, position adjustments, etc.)
        currentMask.ShowMaskOnPlayer(maskHolder);

        // Update the UI to display the current mask
        if (maskUIImage != null && newMask.maskSprite != null)
        {
            maskUIImage.sprite = newMask.maskSprite; // Display the current mask sprite in the UI
        }
    }

    // Equip the mask based on the button clicked
    public void EquipMaskFromButton(Button button)
{
    int index = maskButtons.IndexOf(button);
    if (index >= 0 && index < collectedMasks.Count)
    {
        Debug.Log("Equipping Mask: " + collectedMasks[index].name);  // Debug log
        EquipMask(collectedMasks[index]); // Equip the selected mask
    }
}

    // Collect a mask (add to the list and update the UI)
    public void CollectMask(Mask newMask)
{
    if (!collectedMasks.Contains(newMask))
    {
        collectedMasks.Add(newMask); // Add the mask to the collected list
        Debug.Log("Collected Mask: " + newMask.name);  // Debug log to confirm collection
        UpdateMaskButtons(); // Update UI buttons
    }
}


    private void UpdateMaskButtons()
    {
        // Make sure there are enough buttons for each mask in the collected list
        for (int i = 0; i < collectedMasks.Count; i++)
        {
            // If the button is not active, show it
            if (maskButtons[i] != null)
            {
                maskButtons[i].gameObject.SetActive(true);
                maskButtons[i].GetComponentInChildren<Text>().text = collectedMasks[i].maskPrefab.name; // Display the name of the mask on the button
            }
        }
    }
}
