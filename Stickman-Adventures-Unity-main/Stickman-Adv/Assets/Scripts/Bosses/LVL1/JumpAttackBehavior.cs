using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackBehavior : StateMachineBehaviour
{

    private Transform player;
    public float speed;
    private bool isTurnedLeft;

    private Animator animator;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isTurnedLeft = true;
        this.animator = animator;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            return;
        }
        
        Vector2 target = new Vector2(player.position.x, animator.transform.parent.position.y);
        animator.transform.parent.position = Vector2.MoveTowards(animator.transform.parent.position, target, speed * Time.deltaTime);
        LookAtThePlayer();
    }
    
    private void LookAtThePlayer() 
    {
        bool isPlayerToTheLeftOfTheBoss = player.position.x <= animator.transform.parent.position.x;
        if (isPlayerToTheLeftOfTheBoss && !isTurnedLeft)
        {
            RotateOnY();
            isTurnedLeft = true;            
        } 
        else if (!isPlayerToTheLeftOfTheBoss && isTurnedLeft)
        {
            RotateOnY();
            isTurnedLeft = false;
        }
    }


    private void RotateOnY()
    {
        animator.transform.parent.Rotate(0f, 180f, 0f);
    }


}
