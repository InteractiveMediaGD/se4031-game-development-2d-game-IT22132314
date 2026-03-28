using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlThreeController : EnemyController
{
    private Animator animator;
    private Rigidbody2D rb2D;
    private bool isEnraged;
    private int currentStage; //@TODO: create enum class for this
    private bool isBulletDetected;

    [Header("Distance To Player Settings")]
    public float minimumDistance;
    

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TurnToPlayer();
        if (CurrentLifePoints <= LifePoints / 2 && currentStage == 0)
        {
            animator.SetTrigger("Anger");
            currentStage++;
        }

        if (CurrentLifePoints <= 0) 
        {
            animator.SetTrigger("Die");
        }
    }

    //Called at the end of idle animation
    public void StartRunning()
    {
        animator.SetTrigger("Run");
    }

    public bool IsCloseToThePlayer()   //@TODO: refactor it to be reusable on enemy controller
    {
        return Mathf.Abs(player.position.x - rb2D.position.x) <= minimumDistance;
    }

    public override void UnFreeze()
    {
        animator.SetTrigger("Run");
    }

}
