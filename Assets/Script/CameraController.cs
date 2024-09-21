using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to keep the camera at a distance
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime = 0.3f; // Set a default value for smoothTime

    // Boundaries for the camera
    public float minX, maxX, minY, maxY;

    private CameraShake cameraShake; // Reference to the CameraShake component
    public bool isClamped;

    void Awake()
    {
        isClamped = true;
        // Find the GameObject with the "Player" tag and get its Transform component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found.");
        }

        // Get the CameraShake component from the camera
        cameraShake = GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("No CameraShake component found on this GameObject.");
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

            float clampedX = smoothedPosition.x;
            float clampedY = smoothedPosition.y + 0.5f;

            if (isClamped)
            {
                clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
                clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);
            }
            // Clamp the camera's position within the defined boundaries


            // Apply the clamped position and add the shake offset
            Vector3 finalPosition = new Vector3(clampedX, clampedY, smoothedPosition.z) + cameraShake.GetShakeOffset();

            transform.position = finalPosition;
        }
    }

    public void offClamp()
    {
        isClamped = false;
    }
}
