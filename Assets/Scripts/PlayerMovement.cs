using System.Collections;
using System.Collections.Generic;
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
    public Transform maskHolder;

    private GameObject currentMaskObject; 
    private List<Mask> collectedMasks = new List<Mask>();  

    public List<GameObject> maskPrefabs; 
    private List<GameObject> activeMaskObjects = new List<GameObject>();  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  

        foreach (GameObject maskPrefab in maskPrefabs)
        {
            GameObject maskInstance = Instantiate(maskPrefab, maskHolder.position, Quaternion.identity, maskHolder);
            maskInstance.SetActive(false);  
            activeMaskObjects.Add(maskInstance);
        }
    }

  void Update()
{
    
    moveInput = Input.GetAxis("Horizontal");
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundMask);
    rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    if (isGrounded && Input.GetButtonDown("Jump"))
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Log input key presses
    if (Input.GetKeyDown(KeyCode.F)) { Debug.Log("F key pressed"); }
    if (Input.GetKeyDown(KeyCode.G)) { Debug.Log("G key pressed"); }
    if (Input.GetKeyDown(KeyCode.H)) { Debug.Log("H key pressed"); }

    CheckForMaskSwitch();  // Call CheckForMaskSwitch method in Update
}


void CheckForMaskSwitch()
{
    Debug.Log("Checking for mask switch");  // This log should show if the method is called.

    if (collectedMasks.Count == 0) return;

    if (Input.GetKeyDown(KeyCode.F) && collectedMasks.Count > 0)
    {
        Debug.Log("Switching to Mask F");
        EquipMask(collectedMasks[0]); 
    }
    else if (Input.GetKeyDown(KeyCode.G) && collectedMasks.Count > 1)
    {
        Debug.Log("Switching to Mask G");
        EquipMask(collectedMasks[1]);  
    }
    else if (Input.GetKeyDown(KeyCode.H) && collectedMasks.Count > 2)
    {
        Debug.Log("Switching to Mask H");
        EquipMask(collectedMasks[2]);  
    }
}

    public void EquipMask(Mask newMask)
{
    if (newMask != null)
    {
        if (currentMaskObject != null)
        {
            Mask previousMask = currentMaskObject.GetComponent<Mask>();
            if (previousMask != null)
            {
                previousMask.RemoveEffect(this);  
            }

            currentMaskObject.SetActive(false);
        }

        GameObject newMaskObject = Instantiate(newMask.maskPrefab, maskHolder.position, Quaternion.identity);
        newMaskObject.transform.SetParent(maskHolder);
        currentMaskObject = newMaskObject;

        newMask.ApplyEffect(this);

        newMaskObject.SetActive(true);

        Debug.Log("Equipped Mask: " + newMask.name);
    }
}

    public void CollectMask(Mask newMask)
{
    if (newMask != null && !collectedMasks.Contains(newMask))
    {
        collectedMasks.Add(newMask);
        Debug.Log("Mask collected: " + newMask.name);
        Debug.Log("Total masks collected: " + collectedMasks.Count); 
    }
}
}
