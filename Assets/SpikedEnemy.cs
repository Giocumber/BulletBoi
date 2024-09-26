using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject deathEffect;
    public float knockbackForce = 5f;
    private CameraShake cameraShake;

    public float followRange = 5f; // Range within which enemy will follow player
    public float stopRadius = 1f; // Minimum distance from the player
    public float repositionRadius = 0.5f; // Distance at which enemy will reposition to keep safe distance
    public float moveSpeed = 2f; // Speed of the enemy
    public float randomMoveInterval = 2f; // Time between each random move
    public float repositionDistance = 3f; // Distance to move away when player gets too close

    private Vector2 randomDirection;
    private Vector2 target; // Target position to follow the player
    private bool isFollowingPlayer = false;
    private Transform player;
    public float spikeDamage;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the sprite renderer
        player = GameObject.FindWithTag("Player").transform; // Find the player with tag
        StartCoroutine(RandomMovement());
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; // Make sure player exists

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the enemy should start following the player
        if (distanceToPlayer <= followRange)
        {
            isFollowingPlayer = true;

            if (distanceToPlayer <= repositionRadius)
            {
                // Player is too close, reposition the enemy to maintain a safe distance
                RepositionFromPlayer();

            }
            else if (distanceToPlayer > stopRadius)
            {
                // Player is within range but not too close, follow the player
                FollowPlayer();
            }
        }
        else
        {
            isFollowingPlayer = false;
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player if within range but stop at the stop radius
        target = player.position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        FlipSprite(direction);
    }

    private void RepositionFromPlayer()
    {
        // Move away from the player to maintain a safe distance
        Vector2 directionAway = (transform.position - player.position).normalized;
        target = (Vector2)transform.position + directionAway * repositionDistance;

        // Move to the new target (away from player)
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        FlipSprite(directionAway);
    }
    private void FlipSprite(Vector2 direction)
    {
        // Flip the sprite on the X-axis depending on the movement direction
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = false;// Face right
        }
    }
    private IEnumerator RandomMovement()
    {
        while (true)
        {
            if (!isFollowingPlayer)
            {
                // Generate a random direction for movement
                randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                Vector2 randomTarget = (Vector2)transform.position + randomDirection; // Random position to move towards

                // Move towards the random target
                float elapsedTime = 0f;
                while (elapsedTime < randomMoveInterval && !isFollowingPlayer)
                {
                    transform.position = Vector2.MoveTowards(transform.position, randomTarget, moveSpeed * Time.deltaTime);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait for the next frame
                }
            }
            yield return new WaitForSeconds(randomMoveInterval); // Wait before generating a new random direction
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(spikeDamage);
            Debug.Log(playerHealth.currentHealth);
        }
    }

    public void ApplyKnockback(Transform playerTransform)
    {
        // Calculate the direction of knockback
        Vector2 knockbackDirection = (transform.position.x > playerTransform.position.x) ? Vector2.right : Vector2.left;

        // Apply knockback force in the calculated direction
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
}
