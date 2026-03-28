using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerBehavior : StateMachineBehaviour
{
    private BossLvlThreeController bossController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.GetComponent<BossLvlThreeController>();
        bossController.BecomeUnbeatable();  
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.BecomeBeatable();
    }
    
}
