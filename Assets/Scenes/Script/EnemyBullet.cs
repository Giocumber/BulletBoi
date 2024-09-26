using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject BulletEffect;
    public float bulletDamage;

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
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(bulletDamage);
            Instantiate(BulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall")
        { 
            Instantiate(BulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
