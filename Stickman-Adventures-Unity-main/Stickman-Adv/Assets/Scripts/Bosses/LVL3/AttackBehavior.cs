using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private BossLvlThreeController bossController;
    public string RunTriggerName;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.GetComponent<BossLvlThreeController>();        
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (!bossController.IsCloseToThePlayer())
        {
            animator.SetTrigger(RunTriggerName);
        }       
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(RunTriggerName);     
    }

}
