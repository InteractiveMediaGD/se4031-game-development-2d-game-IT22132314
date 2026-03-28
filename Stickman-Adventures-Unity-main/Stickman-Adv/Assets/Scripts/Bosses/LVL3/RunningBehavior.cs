using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D rigidbody2D;
    BossLvlThreeController bossController;
    public float ReactToBulletProbability;
    public string AttackTriggerName;
    public string ReactToBulletTriggerName;
    protected float NextActionProbability;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossLvlThreeController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int horizontalDirection = bossController.HorizontalMoveTowardsPlayer();
        rigidbody2D.velocity = new Vector2(MovementSpeed * horizontalDirection, rigidbody2D.velocity.y);

        if (bossController.IsCloseToThePlayer())
        {
            animator.SetTrigger(AttackTriggerName);
            return;
        }
        
        NextActionProbability = Random.Range(0, 100);

        if (bossController.IsBulletDetected() && NextActionProbability < ReactToBulletProbability && bossController.IsOnTheGround())
        {
            animator.SetTrigger(ReactToBulletTriggerName);
            return;
        } 
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(AttackTriggerName);
        animator.ResetTrigger(ReactToBulletTriggerName);
    }
}
