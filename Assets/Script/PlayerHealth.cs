using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public GameObject deathEffect;

    public GameObject healthBarObj;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (currentHealth < 1)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void AddHP(float value)
    {
        currentHealth += value;
    }

    public void Die() 
    {
        currentHealth = 0;
        Instantiate(deathEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
        healthBarObj.gameObject.SetActive(false);

    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(20);
            Debug.Log("spikehahaha");
        }
    }
}
