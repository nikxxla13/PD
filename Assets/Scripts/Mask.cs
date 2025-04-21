using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mask : MonoBehaviour
{
    public enum MaskType
    {
        JumpMask,
        SpeedMask,
        SizeMask
    }

    public MaskType maskType;
    public GameObject maskPrefab;  
    public Sprite maskSprite;      

    public abstract void ApplyEffect(Player player);
    public abstract void RemoveEffect(Player player);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // Equip the mask based on its type
                switch (maskType)
                {
                    case MaskType.JumpMask:
                        player.EquipMask(0);
                        break;
                    case MaskType.SpeedMask:
                        player.EquipMask(1);
                        break;
                    case MaskType.SizeMask:
                        player.EquipMask(2);
                        break;
                }

                Destroy(gameObject); // Remove the mask object after pickup
            }
        }
    }

    public void ShowMaskOnPlayer(Transform maskHolder)
    {
        if (maskPrefab != null)
        {
            GameObject maskObject = Instantiate(maskPrefab, maskHolder.position, Quaternion.identity);
            maskObject.transform.SetParent(maskHolder);

            if (maskObject.GetComponent<SpriteRenderer>() != null && maskSprite != null)
            {
                maskObject.GetComponent<SpriteRenderer>().sprite = maskSprite;
            }

            maskObject.transform.localPosition = new Vector3(0, 0, -0.1f);
        }
    }
}