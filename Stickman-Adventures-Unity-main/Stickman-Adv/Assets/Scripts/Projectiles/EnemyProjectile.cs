using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                player.ReceiveDamage(Damage, transform.position.x);
            }
        }

        if (!other.gameObject.CompareTag("Enemy"))
        {
           //Destroy(gameObject, 0.02f);
        }
    }
}
