using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float DurationTime;
    public float Speed;
    public int Damage = 1;
    private Vector2 ShootDirection;
    private float LifeSpanTime;
    protected Rigidbody2D rb2D;   
    protected Renderer render;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();        
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        LifeSpanTime -= Time.deltaTime;

        if (LifeSpanTime < 0) 
        {
            Destroy(gameObject);
        }        
    }

    public void Shoot()
    {
        rb2D.velocity = ShootDirection * Speed;
        LifeSpanTime = DurationTime;
        UpdateSpriteDirection();        
    }

    private void UpdateSpriteDirection()
    {
        if (ShootDirection.x > 0)
        {
            gameObject.transform.Rotate(0f, 180f, 0f);  
        }        
    }

    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}

