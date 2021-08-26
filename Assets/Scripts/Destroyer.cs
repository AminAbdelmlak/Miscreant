using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{


    void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("Projectile") || collision.CompareTag("Background"))
        {
            return;
        }
        else
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Destroyer"))
        {
            Destroy(collision.gameObject);
        }
	}
}
