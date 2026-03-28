using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : StateMachineBehaviour
{
    public string ReactToBulletTriggerName;
    public string AngerRunningTriggerName;
    BossLvlThreeController bossController;
    public float ReactToBulletProbability;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
        bossController = animator.GetComponent<BossLvlThreeController>();    
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float nextActionProbability = Random.Range(0, 100);
        if (bossController.IsBulletDetected() && ReactToBulletProbability < nextActionProbability)
        {
            animator.SetTrigger(ReactToBulletTriggerName);
            return;
        }

        animator.SetTrigger(AngerRunningTriggerName);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(ReactToBulletTriggerName);
        animator.ResetTrigger(AngerRunningTriggerName);
    }
}
