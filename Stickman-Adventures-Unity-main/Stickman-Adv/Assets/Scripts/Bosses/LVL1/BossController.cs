using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private Animator animator;
    public Animator cameraAnimator;

    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {        
        if (CurrentLifePoints <= HalfBossLife() && CurrentLifePoints > 0)
        {
            animator.SetTrigger("rolling");
        }
        else if (IsDead())
        {
            animator.SetTrigger("dead");
        }
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage); // This will add score and check for death bonus
    }

    public void ShakeCamera()
    {
        cameraAnimator.SetTrigger("Earthquake");
    }

    public override void UnFreeze()
    {
        animator.SetTrigger("jump");
    }

}
