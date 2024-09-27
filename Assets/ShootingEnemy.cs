using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject deathEffect;
    private CameraShake cameraShake;

    public float followRange = 5f; // Range within which enemy will follow player
    public float stopRadius = 1f; // Minimum distance from the player
    public float repositionRadius = 0.5f; // Distance at which enemy will reposition to keep safe distance
    public float moveSpeed = 2f; // Speed of the enemy
    public float randomMoveInterval = 2f; // Time between each random move
    public float repositionDistance = 3f; // Distance to move away when player gets too close
    public float enemyAttackCD;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    private Vector2 randomDirection;
    private Vector2 target; // Target position to follow the player
    private bool isFollowingPlayer = false;
    private Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
       // bulletSpawnPoint = GameObject.Find("BulletSpawnPoint").transform;
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the sprite renderer
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Find the player with tag
        StartCoroutine(RandomMovement());
    }

    // Update is called once per frame
    void Update()
    {

        // Continuously run the shooting coroutine
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
                target = new Vector2(player.position.x, player.position.y);
                if (!IsInvoking("StartShooting"))
                {
                    InvokeRepeating("StartShooting", 0f, enemyAttackCD);
                }
            }
        }
        else
        {

            isFollowingPlayer = false;
        }
    }

    private void FollowPlayer()
    {    // Move towards the player if within range but stop at the stop radius
        target = player.position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        rb.MovePosition(Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime));
        //Change moving using physics (Rigidbody2D)
        FlipSprite(direction);
    }

    private void RepositionFromPlayer()
    {
        // Move away from the player to maintain a safe distance
        Vector2 directionAway = (transform.position - player.position).normalized;
        target = (Vector2)transform.position + directionAway * repositionDistance;

        // Move to the new target (away from player) using Rigidbody2D to respect collisions
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime));
        //Change moving using physics (Rigidbody2D)
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.AddHP(20); // Unique behavior for the player
        }

        // Common behavior for both "bullet" and "Player"
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player"))
        {
            cameraShake.TriggerShake();
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void StartShooting()
    {
        StartCoroutine(EnemyShootCD());
    }

    IEnumerator EnemyShootCD()
    {
        // Wait for the cooldown before shooting
        yield return new WaitForSeconds(enemyAttackCD);

        if (gameObject != null)
            Shoot();
    }

    void Shoot()
    { // Get the player's current position at the time of shooting
        Vector3 playerPosition = player.position; // Directly target player's position for shooting

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity); // Spawn bullet without affecting its rotation

        // Get the bullet's Rigidbody2D component
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Calculate the direction from the bullet spawn point to the player's position
        Vector2 directionToPlayer = (new Vector2(playerPosition.x, playerPosition.y) - new Vector2(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y)).normalized;

        // Set the bullet's velocity to move toward the player
        bulletRb.velocity = directionToPlayer * bulletSpeed;

        // Rotate the bullet to face the direction it's moving
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle); // Adjust rotation to face direction of travel
    }
}
