using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    //public float knockbackForce = 5f;

    private Rigidbody2D rb;
    Rigidbody2D bulletRb;

    private GameObject bullet;
    public float bulletAttackCD;
    public float teleportCD;

    public bool canShoot;

    private PlayerHealth playerHealth;
    public float BulletHealthReduct;
    private CameraZoom cameraZoom;
    private CheckEnemies checkEnemies;

    private SceneManagerScript sceneManagerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        cameraZoom = GameObject.Find("MainCamera").GetComponent<CameraZoom>();
        canShoot = true;

        checkEnemies = GameObject.Find("UI_Manager").GetComponent<CheckEnemies>();
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    void Update()
    {
        if (CanShoot())
        {
            StartCoroutine(ShootCooldown());
        }

        //&& bullet != null && !checkEnemies.allEnemiesDestroyed
        if (Input.GetKeyDown(KeyCode.Space) && bullet != null && !checkEnemies.allEnemiesDestroyed)
        {
            StartCoroutine(TeleportCooldown());
        }
    }

    #region SHOOT CONDITIONS
    bool CanShoot()
    {
        return IsLeftClick() && IsGameActive() && !IsPointerOverUI();
    }

    bool IsLeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    bool IsGameActive()
    {
        return canShoot && !checkEnemies.allEnemiesDestroyed && !sceneManagerScript.isPaused;
    }

    bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    #endregion

    IEnumerator ShootCooldown()
    {
        Shoot();
        canShoot = false;
        yield return new WaitForSeconds(bulletAttackCD);
        canShoot = true;
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

    }

    //public void KnockBack()
    //{
    //    rb.AddForce(transform.position * knockbackForce, ForceMode2D.Impulse);
    //}

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
