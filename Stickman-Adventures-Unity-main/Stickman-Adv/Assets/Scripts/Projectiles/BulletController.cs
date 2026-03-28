using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Projectile
{

    [Header("Hit Effect Settings")]
    public GameObject ParticleExplosion;
    public GameObject HitExplosion;
    public AudioSource Explosion;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && render.enabled)
        {
            EnemyController enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
            Instantiate(ParticleExplosion, transform.position, Quaternion.identity);
            Instantiate(HitExplosion, transform.position, Quaternion.identity);
            Explosion.Play();
            
            if (enemy != null)
            {
                enemy.ReceiveDamage(Damage);
            }
        }

        if (!other.gameObject.CompareTag("Player"))
        {
            render.enabled = false;
            Destroy(gameObject, 1f);
        }
    }

}
