using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour {
    public float speed = 2f; // Speed of movement
    public float distance = 3f; // How far it moves from its starting point

    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    void Update() {
        float movement = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = new Vector3(startPos.x, startPos.y + movement, startPos.z);
    }
}