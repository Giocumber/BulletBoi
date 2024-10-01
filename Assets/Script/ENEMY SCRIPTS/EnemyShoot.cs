using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyShoot : MonoBehaviour
{
 

    public float enemyAttackCD;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        target = new Vector2(player. position.x, player.position.y);
    }
    void Update()
    {
        target = new Vector2(player.position.x, player.position.y);
        // Continuously run the shooting coroutine
        if (!IsInvoking("StartShooting"))
        {
            InvokeRepeating("StartShooting", 0f, enemyAttackCD);
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
        Shoot();
    }

    void Shoot()
    {
        // Get the player's current position at the time of shooting
        Vector3 playerPosition = target;

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the bullet's Rigidbody2D component
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Calculate the direction from the bullet spawn point to the player's position
        Vector2 directionToPlayer = (new Vector2(playerPosition.x, playerPosition.y) - new Vector2(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y)).normalized;

        // Set the bullet's velocity to move toward the player
        bulletRb.velocity = directionToPlayer * bulletSpeed;

        // Rotate the bullet to face the direction it's moving
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
