using UnityEngine;

public class PlayerTilt : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public float tiltSpeed = 5f; // Speed at which the player tilts
    public float maxTiltAngle = 80f; // Maximum angle for the tilt

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float velocity = rb.velocity.x; // Get the player's horizontal velocity
        float targetAngle = 0f; // Default target angle is 0 (resting state)

        // Determine the target angle based on velocity
        if (velocity > 0) // Moving right
        {
            targetAngle = -maxTiltAngle; // Tilt to -80 degrees when velocity is positive
        }
        else if (velocity < 0) // Moving left
        {
            targetAngle = maxTiltAngle; // Tilt to 80 degrees when velocity is negative
        }

        // Smoothly rotate the player towards the target angle
        float currentAngle = transform.rotation.eulerAngles.z;
        if (currentAngle > 180) currentAngle -= 360; // Convert angle to -180 to 180 range for easier interpolation
        float newAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * tiltSpeed);

        // Apply the new rotation
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }
}
