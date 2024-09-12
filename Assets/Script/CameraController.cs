using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the ball's transform
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to keep the camera at a distance
    Vector3 velocity = Vector3.zero;
    [Range(0, 1)]
    public float smoothTime = 0.3f; // Set a default value for smoothTime

    void Awake()
    {
        // Find the GameObject with the "ball" tag and get its Transform component
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        if (ball != null)
        {
            target = ball.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'player' found.");
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with the offset
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
