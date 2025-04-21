using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public float speed = 2f; // Speed of movement
    public float distance = 3f; // How far it moves from its starting point

    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        float movement = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = new Vector3(startPos.x + movement, startPos.y, startPos.z);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Stick player to platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // Unstick player from platform
            collision.transform.SetParent(null);
        }
    }
}