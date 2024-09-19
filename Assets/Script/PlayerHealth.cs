using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float BulletHealthreduct;
    public GameObject deathEffect;

    private void Start()
    {
        currentHealth = 100;
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
        currentHealth -= 100;
        Instantiate(deathEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }
}
