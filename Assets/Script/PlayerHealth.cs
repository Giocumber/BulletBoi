using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public GameObject deathEffect;

    public GameObject healthBarObj;

    private CheckEnemies checkEnemies;
    private SlowMotion slowMotion;

    private void Start()
    {
        checkEnemies = GameObject.Find("UI_Manager").GetComponent<CheckEnemies>();
        slowMotion = GetComponent<SlowMotion>();
    }

    private void Update()
    {
        if (currentHealth < 1)
        {
            Die();
        }

        if (currentHealth > 100)
        {
            SetMaximumHealth();
        }


    }

    void SetMaximumHealth()
    {
        currentHealth = maxHealth;
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
        slowMotion.DeactivateSlowMotion();
        checkEnemies.LevelFailed();
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }
}
