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

    // Boundaries for the camera
    public float minX, maxX, minY, maxY;

    void Awake()
    {
        // Find the GameObject with the "Player" tag and get its Transform component
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        if (ball != null)
        {
            target = ball.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found.");
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with the offset
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // Clamp the camera's position within the defined boundaries
            float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Apply the clamped position to the camera
            transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
        }
    }
}
