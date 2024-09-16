using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private bool facingRight = true;
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

        // Apply the rotation to the sprite
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
