using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    public GameObject deathEffect;

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
        if (collision.gameObject.tag == "bullet")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            //player health -- 
            //knock back 
        }   


    }
}
