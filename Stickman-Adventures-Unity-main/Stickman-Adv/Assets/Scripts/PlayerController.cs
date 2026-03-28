using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    private bool IsPlayerOnTheGround = false;
    Animator animator;
    private bool isFrozen;

    [Header("Moving Settings")]
    public float MovementSpeed;
    private bool isTurnedRight;
    private float horizontalMovement;

    [Header("Jumping Settings")]
    public float JumpSpeed;
    public int NumberOfJumps;
    private bool isJumping;
    private int currentNumberOfJumps;

    [Header("Shooting Settings")]
    public int Damage;
    public GameObject bulletPrefab;
    public Transform bulletGizmod;
    private bool isShooting;
    private bool isShootingKeyPressed;
    private bool isShootingKeyReleased;
    private float shootingStartInstant;


    [Header("Life Settings")]
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    public bool IsTakingDamage;

    [Header("Sound Effect Settings")]
    public AudioSource DefeatSound;

    [Header("Dash Settings")]
    public float IntervalBetweenTapsForRollingDash;
    public float rollingDistance;
    private bool isRolling;
    private float doubleTapCoolDown;
    private KeyCode lastHorizontalKeyCodeMovement;

    [Header("Defending Settings")]
    public float DefendingCoolDown;
    public int DefendingAvailableLevel;
    private bool isDefending;
    private bool isDefendingKeyPressed;
    private float defendingTimer;

    void Start()
    {
        defendingTimer = DefendingCoolDown;
        // Allow the player to pass THROUGH enemies instead of being physically blocked
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (playerLayer != -1 && enemyLayer != -1) {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        }

        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        isTurnedRight = true;
        CurrentLifePoints = LifePoints;
        currentNumberOfJumps = NumberOfJumps;
    }

    void Update()
    {
        if (isDefending)
        {
            UpdateDefending();
            return;
        }
        
        if (IsTakingDamage || isRolling || isFrozen) 
        {
            return;
        }

        UpdateInputs();
        UpdateShooting();
        UpdateMovement();   
        UpdateAnimation();
        UpdateDirection();
        UpdateDefending();
    }

    private void UpdateInputs()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetKeyDown(KeyCode.Space);
        isShootingKeyPressed = Input.GetMouseButtonDown(0);
        isDefendingKeyPressed = Input.GetKeyDown(KeyCode.V);        
    }

    private void FixedUpdate() 
    {         
        DetectIfPlayerIsOnTheGround();
    }
    
    private void UpdateMovement()
    {
  
        Rigidbody2D.velocity = new Vector2(horizontalMovement * MovementSpeed, Rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryDash(KeyCode.A);
        } 
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryDash(KeyCode.D);
        }

        if ((IsPlayerOnTheGround && isJumping) || (isJumping && !IsPlayerOnTheGround && currentNumberOfJumps > 1)) 
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpSpeed);
            currentNumberOfJumps--;
        } 
    }

    private void TryDash(KeyCode keyCode)
    {
        if (IsPlayerOnTheGround && doubleTapCoolDown > Time.time && lastHorizontalKeyCodeMovement == keyCode)
        {
            StartCoroutine(RollDash());
        }
        else
        {
            doubleTapCoolDown = CalculateDoubleTapCoolDown();
        }
        lastHorizontalKeyCodeMovement = keyCode;
    }

    private float CalculateDoubleTapCoolDown()
    {
        return Time.time + IntervalBetweenTapsForRollingDash;
    }

    private IEnumerator RollDash()
    {
        isRolling = true;
        Rigidbody2D.AddForce(new Vector2(rollingDistance * horizontalMovement, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        isRolling = false;
    }

    private void UpdateDefending()
    {
        if (DefendingAvailableLevel > LevelStateHolder.CurrentLevel) 
        {
            return;
        }
        if (isDefending)
        {
            defendingTimer -= Time.deltaTime;
        }
        
        if (defendingTimer <= 0 && isDefending)
        {
            isDefending = false;
            defendingTimer = DefendingCoolDown;
            return;
        }
        
        if (isDefendingKeyPressed && !isDefending)
        {
            Defend();
        }
    }

    private void UpdateAnimation()
    {
        string nextAnimationName = "";

        if (isDefending)
        {
            nextAnimationName = "Player_Defend";
        }
        else if (isRolling)
        {
            nextAnimationName = "Player_Rolling";
        }
        else if (IsPlayerOnTheGround) 
        {
            if (horizontalMovement != 0)
            {
                nextAnimationName = isShooting ? "Player_Run_Shoot" : "Player_Run";
            } 
            else
            {
                nextAnimationName = isShooting ? "Player_Idle_Shoot" : "Player_Idle";
            }
        } 
        else 
        {
            nextAnimationName = isShooting ? "Player_Jump_Shoot" : "Player_Jump";            
        }

        animator.Play(nextAnimationName);
    }

    private void UpdateShooting()
    {
        if (isShootingKeyPressed && !isShootingKeyReleased)
        {
            isShooting = true;
            isShootingKeyReleased = true;
            shootingStartInstant = Time.time;
            Invoke("Shoot", 0.1f);
        }

        if (isShooting && isShootingKeyReleased)
        {
            float shootingTimeLength = Time.time - shootingStartInstant;

            if (shootingTimeLength >= 0.35f)
            {
                isShooting = false;
                isShootingKeyReleased = false;               
            }
        }        
    }

    private void UpdateDirection() 
    {
        if ((horizontalMovement > 0 && !isTurnedRight) || (horizontalMovement < 0 && isTurnedRight))
        {
            RotateY();
        }
    }

    private void RotateY()
    {
        isTurnedRight = !isTurnedRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void DetectIfPlayerIsOnTheGround() 
    {
        IsPlayerOnTheGround = false;

        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        float groundDetectionDistance = 0.04f;

        Vector3 boxColliderCenter = BoxCollider2D.bounds.center;
        boxColliderCenter.y = BoxCollider2D.bounds.min.y + (BoxCollider2D.bounds.extents.y / 2f);
        
        Vector3 boxSize = BoxCollider2D.bounds.size;
        float collisionAngle = 0f;
        Vector2 collisionDetectionDirection = Vector2.down;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderCenter, boxSize, collisionAngle, collisionDetectionDirection, groundDetectionDistance, groundLayer);

        if (raycastHit2D.collider != null) 
        {
            IsPlayerOnTheGround = true;
            currentNumberOfJumps = NumberOfJumps;
        }
    }

    void Shoot() 
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletGizmod.position, Quaternion.identity);
        bullet.name = bulletPrefab.name;

        BulletController bulletController = bullet.GetComponent<BulletController>();
        Vector2 shootDirection = isTurnedRight ? Vector2.right : Vector2.left;
        bulletController.SetShootDirection(shootDirection);
        bulletController.Damage = this.Damage;
        bulletController.Shoot();
    }

    public void RecoverHealth(int healthAmount)
    {
        CurrentLifePoints += healthAmount;
        if (CurrentLifePoints > LifePoints)
        {
            CurrentLifePoints = LifePoints;
        }
        updateHealthbar();
    }

    //Damage related methods
    public void ReceiveDamage(int damageReceived, float enemyHorizontalPosition)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damageReceived;
            updateHealthbar();
            HandleReceivedDamage(enemyHorizontalPosition);
        }
    }

    private void updateHealthbar() 
    {
        float remainingLifePointsPercentage = CurrentLifePoints / (float) LifePoints;
        Healthbar.HealthbarSingleton.SetValue(remainingLifePointsPercentage); 
    }

    public void HandleReceivedDamage(float enemyHorizontalPosition)
    {
        if (CurrentLifePoints <= 0)
        {
            DefeatSound.Play();
        }
        else if (!IsTakingDamage)
        {
            IsTakingDamage = true;
            IsInvincible = true;
            PushPlayerWhenIsTakingDamage(enemyHorizontalPosition);
            animator.Play("Player_Hit");
        }
    }

    private void PushPlayerWhenIsTakingDamage(float enemyHorizontalPosition) 
    {
        int hitDirection = (transform.position.x > enemyHorizontalPosition) ? 1 : -1;
        Vector2 forceDirection = new Vector2(17.50f * hitDirection, 3.50f);
        
        Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    //Called by animation event in Player_Hit animation
    public void StopHitAnimation()
    {
        IsTakingDamage = false;
        IsInvincible = false;
    }

    public bool IsPlayerDead()
    {
        return CurrentLifePoints <= 0;
    }

    public void Freeze()
    {
        animator.Play("Player_Idle");
        isFrozen = true;
    }

    public void UnFreeze()
    {
        isFrozen = false;
    }

    public bool IsDefendingAvailable()
    {
        return LevelStateHolder.CurrentLevel >= DefendingAvailableLevel;
    }

    public void Defend()
    {
        IsInvincible = true;
        isDefending = true;
        animator.Play("Player_Defend");
    }

    //Called by animation event in Player_Defend animation
    public void StopDefending()
    {
        IsTakingDamage = false;
        IsInvincible = false;
    }    
}
