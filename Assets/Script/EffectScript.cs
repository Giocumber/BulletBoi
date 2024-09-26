using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public GameObject BulletEffect;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BasicEnemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "ShootingEnemy")
        {
            Instantiate(BulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            cameraShake.TriggerShake();
            BasicEnemy basicEnemy = collision.gameObject.GetComponent<BasicEnemy>();
            Instantiate(basicEnemy.deathEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
