using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private CameraShake cameraShake;
    public float collisionBoost = 4f;
    public float hpAdd = 30f;

    private AudioManager audioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            if (!audioManager.SFXBiteSource.isPlaying)
                audioManager.PlayBiteSFX();
            //Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * collisionBoost;

            playerHealth.AddHP(hpAdd); // Unique behavior for the player
            cameraShake.TriggerShake();
            BasicEnemy basicEnemy = collision.gameObject.GetComponent<BasicEnemy>();
            Instantiate(basicEnemy.deathEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
