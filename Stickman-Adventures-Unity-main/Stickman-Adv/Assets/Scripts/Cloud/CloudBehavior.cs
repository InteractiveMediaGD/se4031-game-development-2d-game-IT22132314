using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb2D;
    private float deadEndX;
    private SpriteRenderer sprite;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.x > deadEndX)
        {
            Destroy(gameObject);
        }        
    }    
    public void Move(float speed, float deadEndX, float scale, float xPosition, float yPosition, float alpha)
    {
        this.rb2D.velocity = Vector3.right * speed;
        this.deadEndX = deadEndX;

        SetPosition(xPosition, yPosition);
        SetScale(scale);
        SetAlpha(alpha);
    }


    private void SetPosition(float xPosition, float yPosition)
    {
        transform.position = new Vector2(xPosition, yPosition);
    }

    private void SetScale(float scale)
    {
        transform.localScale = new Vector2(scale, scale);
    }

    private void SetAlpha(float alpha)
    {
        Color color = sprite.color;
        color.a = Mathf.Clamp(alpha, 0, 1);
        sprite.color = color;
    }

}
