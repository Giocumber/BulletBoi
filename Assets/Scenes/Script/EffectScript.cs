using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public GameObject BulletEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BasicEnemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "ShootingEnemy")
        {
            Instantiate(BulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


}
