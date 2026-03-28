using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{

    public float duration;
    private float passedTime;
    
    private BossController boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        boss.BecomeBeatable();

        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }

        passedTime = duration;        
    
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        passedTime -= Time.deltaTime;
        if (passedTime <= 0)
        {
            animator.SetTrigger("rolling");
        }
        else if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }    
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }
        boss.BecomeUnbeatable();
    }
    
}
