using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int SwordDamage;

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            float enemyHorizontalPosition = transform.position.x;
            player.ReceiveDamage(SwordDamage, enemyHorizontalPosition);
        }
    }

}
