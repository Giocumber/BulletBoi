using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject deathEffect;
    public PlayerHealth playerHealth;
    public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
