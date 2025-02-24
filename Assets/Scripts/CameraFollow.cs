using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Reference to the player's transform
    public float followSpeed = 5f;     // Speed at which the camera follows the player
    public Vector3 offset;            // Offset from the player (camera's position relative to the player)

    void Update()
    {
        // Smoothly move the camera towards the player's position with the defined offset
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
