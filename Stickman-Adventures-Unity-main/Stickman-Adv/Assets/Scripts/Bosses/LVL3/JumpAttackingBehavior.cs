using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackingBehavior : StateMachineBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Transform player;
    private float jumpingForceDistance;
    private Transform bossTransform;

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jumpingForceDistance = player.position.x;
        bossTransform = animator.transform;
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
    
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        while (bossTransform.position.x > jumpingForceDistance)
        {
            rigidbody2D.AddForce(Vector2.up * 45 * Time.fixedDeltaTime, ForceMode2D.Impulse);

        }
        rigidbody2D.AddForce(Vector2.left * 45 * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }


}
