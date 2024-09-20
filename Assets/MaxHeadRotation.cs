using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeadRotation : MonoBehaviour
{
    //public float minZRotation = 209f; // Min rotation in degrees
    //public float maxZRotation = 320f; // Max rotation in degrees

    //void Update()
    //{
    //    // Get the current rotation on the Z-axis
    //    float currentRotationZ = transform.eulerAngles.z;

    //    // Convert the Z rotation to a range of -180 to 180 for easier clamping
    //    if (currentRotationZ > 180f)
    //    {
    //        currentRotationZ -= 360f;
    //    }

    //    // Clamp the rotation between the minimum and maximum rotation
    //    float clampedZRotation = Mathf.Clamp(currentRotationZ, minZRotation - 360f, maxZRotation);

    //    // Apply the clamped rotation back to the GameObject
    //    transform.rotation = Quaternion.Euler(0f, 0f, clampedZRotation);
    //}

    private bool facingRight = true;
    public float minAngle = -30f; // Minimum angle to clamp
    public float maxAngle = 30f;  // Maximum angle to clamp

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the screen position to world position
        mouseScreenPosition.z = .1f; // Adjust if necessary (distance from camera)
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // Calculate the direction from the sprite to the mouse position
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Calculate the angle to rotate the sprite towards the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp the angle to be within the specified bounds

        //angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Apply the rotation to the sprite
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the sprite based on the mouse position
        FlipSprite(mouseWorldPosition.x);
    }

    void FlipSprite(float mouseXPosition)
    {
        // Check if the mouse is on the right or left side of the player
        if (mouseXPosition < transform.position.x && facingRight)
        {
            // Mouse is to the left, flip the sprite
            transform.localScale = new Vector3(-1f, -1f, 1f); // Flip on the X-axis
            facingRight = false;
        }
        else if (mouseXPosition > transform.position.x && !facingRight)
        {
            // Mouse is to the right, set the sprite to face right
            transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale
            facingRight = true;
        }
    }
}