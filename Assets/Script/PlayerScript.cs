using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float knockbackForce = 5f;

    private Rigidbody2D rb;
    Rigidbody2D bulletRb;

    private GameObject bullet;
    public float bulletAttackCD;
    public float teleportCD;

    public bool canShoot;

    public PlayerHealth playerHealth;
    public float BulletHealthReduct;

    public CameraZoom cameraZoom;

    private GameObject otherObj;
    private CheckEnemies checkEnemies;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;

        otherObj = GameObject.Find("UI_Manager");
        if (otherObj != null)
            checkEnemies = otherObj.GetComponent<CheckEnemies>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Cooldown());
        }

        if (Input.GetKeyDown(KeyCode.Space) && bullet != null & !checkEnemies.allEnemiesDestroyed)
        {
            StartCoroutine(TeleportCooldown());
        }
    }

    IEnumerator Cooldown()
    {
        Shoot();
        canShoot = false;
        yield return new WaitForSeconds(bulletAttackCD);
        canShoot = !checkEnemies.allEnemiesDestroyed;
    }

    IEnumerator TeleportCooldown()
    {
        yield return new WaitForSeconds(teleportCD);
        TeleportToBullet();
        cameraZoom.StartZoomIn(cameraZoom.targetZoom);
        Destroy(bullet);
    }

    public void Shoot()
    {
        playerHealth.TakeDamage(BulletHealthReduct);

        bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = 10f; // Adjust this value based on your setup (distance from camera)
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 shootDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;

        bulletRb = bullet.GetComponent<Rigidbody2D>();
        // Set the bullet's velocity
        bulletRb.velocity = shootDirection * bulletSpeed;

        // Rotate the bullet to face the shoot direction
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        //apply knockback
        KnockBack();
    }

    public void KnockBack()
    {
        rb.AddForce(-bulletSpawnPoint.right * knockbackForce, ForceMode2D.Impulse);
    }

    void TeleportToBullet()
    {
        if (bullet != null)
        {
            transform.position = bullet.transform.position; // Teleport to the bullet's position
            rb.velocity = bulletRb.velocity;
        }
        else
        {
            Debug.LogWarning("Bullet is not assigned or has already been destroyed.");
        }
    }

}
