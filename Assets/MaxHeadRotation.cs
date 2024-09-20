using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHeadRotation : MonoBehaviour
{
    //private bool facingRight = true;
    //public float minAngle = -30f; // Minimum angle to clamp
    //public float maxAngle = 30f;  // Maximum angle to clamp

    //void Update()
    //{
    //    // Get the mouse position in screen space
    //    Vector3 mouseScreenPosition = Input.mousePosition;

    //    // Convert the screen position to world position
    //    mouseScreenPosition.z = .1f; // Adjust if necessary (distance from camera)
    //    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

    //    // Calculate the direction from the sprite to the mouse position
    //    Vector2 direction = (mouseWorldPosition - transform.position).normalized;

    //    // Calculate the angle to rotate the sprite towards the mouse
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    // Clamp the angle to be within the specified bounds

    //    //angle = Mathf.Clamp(angle, minAngle, maxAngle);

    //    // Apply the rotation to the sprite
    //    transform.rotation = Quaternion.Euler(0f, 0f, angle);

    //    // Flip the sprite based on the mouse position
    //    FlipSprite(mouseWorldPosition.x);
    //}

    //void FlipSprite(float mouseXPosition)
    //{
    //    // Check if the mouse is on the right or left side of the player
    //    if (mouseXPosition < transform.position.x && facingRight)
    //    {
    //        // Mouse is to the left, flip the sprite
    //        transform.localScale = new Vector3(-1f, -1f, 1f); // Flip on the X-axis
    //        facingRight = false;
    //    }
    //    else if (mouseXPosition > transform.position.x && !facingRight)
    //    {
    //        // Mouse is to the right, set the sprite to face right
    //        transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale
    //        facingRight = true;
    //    }
    //}


    //public Transform playerBody;  // Reference to the player body for determining flip
    //public float maxRotationAngle = 45f;  // Limit for the head rotation
    //public float rotationSpeed = 5f;  // Speed at which the head turns

    //void Update()
    //{
    //    // Get the mouse position in world space
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = 0f;  // We only care about the 2D position (x, y)

    //    // Get direction from the head to the mouse
    //    Vector3 direction = mousePos - transform.position;
    //    float angleToMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    // Limit the head rotation within the max allowed angle
    //    float clampedAngle = Mathf.Clamp(angleToMouse, -maxRotationAngle, maxRotationAngle);

    //    // Smoothly rotate the head towards the target angle
    //    Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    //}

    public Transform playerBody;  // Reference to the player body for determining flip
    public float maxRotationAngle = 45f;  // Limit for the head rotation
    public float rotationSpeed = 5f;  // Speed at which the head turns

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;  // We only care about the 2D position (x, y)

        // Get direction from the head to the mouse
        Vector3 direction = mousePos - transform.position;
        float angleToMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check the scale of the playerBody (parent object) to determine if it's flipped
        if (playerBody.localScale.x < 0)
        {
            // Player is flipped, invert the head rotation logic
            angleToMouse = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        }

        // Limit the head rotation within the max allowed angle
        float clampedAngle = Mathf.Clamp(angleToMouse, -maxRotationAngle, maxRotationAngle);

        // Smoothly rotate the head towards the target angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
