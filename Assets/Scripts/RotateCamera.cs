using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Speed at which the camera rotates
    public float RotationSpeed = 10.0f;

    // Update is called once per frame
    private void Update()
    {
        // Get horizontal input from the player (e.g., arrow keys or A/D)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the camera around the Y-axis based on horizontal input
        transform.Rotate(Vector3.up * (Time.deltaTime * RotationSpeed * horizontalInput));
    }
}
