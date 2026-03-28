using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected bool isPlayerToTheLeft;
    protected bool isTurnedLeft;
    protected BoxCollider2D boxCollider2D;
    
    [Header("Life Settings")]
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    protected Transform player;

    [Header("Touch Damage Settings")]
    public int TouchDamage;

    [Header("Shooting Settings")]
    public float ShootDistanceView;

    protected void Start()
    {
        CurrentLifePoints = LifePoints;        
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider2D = GetComponent<BoxCollider2D>();
        isTurnedLeft = true;
        isPlayerToTheLeft = true;
    }

    public virtual void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
            UpdateHealthbar();
            
            if (ScoreManager.Instance != null) {
                ScoreManager.Instance.AddScore(10); // Give 10 points per hit
                if (CurrentLifePoints <= 0) {
                    ScoreManager.Instance.AddScore(90); // Give 90 bonus points for defeating them
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            float enemyHorizontalPosition = transform.position.x;
            player.ReceiveDamage(TouchDamage, enemyHorizontalPosition);
        }
    }

    public void TurnToPlayer()
    {   
        isPlayerToTheLeft = player.transform.position.x < transform.position.x;

        if ( (isPlayerToTheLeft && !isTurnedLeft) || (!isPlayerToTheLeft && isTurnedLeft)  )
        {
            transform.Rotate(0f, 180f, 0f);
            isTurnedLeft = !isTurnedLeft;
        }
    }

    public int HorizontalMoveTowardsPlayer()
    {
        return isPlayerToTheLeft ? - 1 : 1;
    }

    public bool IsOnTheGround() 
    {
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        float groundDetectionDistance = 0.05f;

        Vector3 boxColliderCenter = boxCollider2D.bounds.center;
        boxColliderCenter.y = boxCollider2D.bounds.min.y + boxCollider2D.bounds.extents.y;
        
        Vector3 boxSize = boxCollider2D.bounds.size;
        float collisionAngle = 0f;
        Vector2 collisionDetectionDirection = Vector2.down;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderCenter, boxSize, collisionAngle, collisionDetectionDirection, groundDetectionDistance, groundLayer);

        if (raycastHit2D.collider != null) 
        {
           return true;
        }

        return false;
    }

    public bool IsBulletDetected()
    {
        Vector2 boxScale = new Vector2(0.01f , transform.localScale.y );
        Vector2 direction = Vector2.right * HorizontalMoveTowardsPlayer();
        float horizontalLengthCollider = (transform.localScale.x / 2) * HorizontalMoveTowardsPlayer() ;
        float yBoxStartPosition = boxCollider2D.bounds.max.y;
        Vector2 startPosition = new Vector2(transform.position.x + ShootDistanceView * HorizontalMoveTowardsPlayer(), yBoxStartPosition);

        RaycastHit2D hitInfo = Physics2D.BoxCast(startPosition, boxScale, 0f,  direction, ShootDistanceView);
 
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Bullet"))
        {   
            return true;
        }

        return false;
    }
    
    public bool GetIsTurnedLeft()
    {
        return isTurnedLeft;
    }

    protected void UpdateHealthbar() {
        float remainingLifePointsPercentage = CurrentLifePoints / (float) LifePoints;
        BossHealthBar.BossHealthbarSingleton.SetValue(remainingLifePointsPercentage);
    }

    public float HalfBossLife()
    {
        return LifePoints / 2;
    }

    public void BecomeUnbeatable()
    {
        IsInvincible = true;
    }

    public void BecomeBeatable()
    {
        IsInvincible = false;
    }

    public bool IsDead()
    {
        return CurrentLifePoints <= 0;
    }

    public abstract void UnFreeze();


}
