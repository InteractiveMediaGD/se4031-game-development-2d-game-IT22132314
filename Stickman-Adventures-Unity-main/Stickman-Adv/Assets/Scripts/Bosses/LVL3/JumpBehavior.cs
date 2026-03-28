using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour
{
    public float JumpSpeed;
    private Rigidbody2D rigidbody2D;
    BossLvlThreeController bossController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(0, JumpSpeed);
        bossController = animator.GetComponent<BossLvlThreeController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (bossController.IsOnTheGround())
        {
            animator.SetTrigger("Run");    
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Run");
    }

}
