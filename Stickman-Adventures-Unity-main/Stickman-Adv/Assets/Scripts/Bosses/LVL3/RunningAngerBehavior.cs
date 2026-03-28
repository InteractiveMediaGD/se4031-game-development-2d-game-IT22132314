using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAngerBehavior : RunningBehavior
{
    public string ShootFireballTriggerName;
    public int ShootFireBallProbability;
    public static float ShootCoolDown = 14;
    private float shootCurrentCoolDown = ShootCoolDown;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        shootCurrentCoolDown -= Time.fixedDeltaTime;

        if (NextActionProbability < ShootFireBallProbability && shootCurrentCoolDown <= 0)
        {
            shootCurrentCoolDown = ShootCoolDown;
            animator.SetTrigger(ShootFireballTriggerName);
        }
    
    }
   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);    
        animator.ResetTrigger(ShootFireballTriggerName);
    }

}
