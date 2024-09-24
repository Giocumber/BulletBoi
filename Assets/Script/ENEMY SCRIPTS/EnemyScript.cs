
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject deathEffect;
    public PlayerHealth playerHealth;
    public CameraShake cameraShake;

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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void RepositionFromPlayer()
    {
        // Move away from the player to maintain a safe distance
        Vector2 directionAway = (transform.position - player.position).normalized;
        target = (Vector2)transform.position + directionAway * repositionDistance;

        // Move to the new target (away from player)
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.AddHP(20); // Unique behavior for the player
        }

        // Common behavior for both "bullet" and "Player"
        if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Player"))
        {
            cameraShake.TriggerShake();
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}