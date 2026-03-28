using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBehavior : StateMachineBehaviour
{
    private Transform playerTransform;
    public float rollingSpeed = 3f;
    private bool isGoingLeft;
    private Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask GroundLayer;
    public bool IsTouchingWall;
    private Transform leftWall;
    private Transform rightWall;
    private Transform targetTransform;
    private BossController boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        boss.BecomeUnbeatable();

        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        wallCheck = GameObject.FindGameObjectWithTag("WallCheck").GetComponent<Transform>();

        leftWall = GameObject.FindGameObjectWithTag("LeftWall").GetComponent<Transform>();
        rightWall = GameObject.FindGameObjectWithTag("RightWall").GetComponent<Transform>();
        
        pickTheFirstDirection(animator);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerTransform == null)
        {
            return;
        }

        IsTouchingWall = Mathf.Abs(wallCheck.position.x - targetTransform.position.x) < 0.5f;

        if (IsTouchingWall) 
        {
            isGoingLeft = !isGoingLeft;
            updateTargetTransform(animator);
        }

        Vector2 target = new Vector2(targetTransform.position.x, animator.transform.parent.position.y);
        animator.transform.parent.position = Vector2.MoveTowards(animator.transform.parent.position, target, rollingSpeed * Time.deltaTime);
        rollingSpeed += Time.deltaTime * 0.25f;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.BecomeBeatable();
    }

    void pickTheFirstDirection(Animator animator) {
        var leftWallX = leftWall.position.x;
        var rightWallX = rightWall.position.x;

        var gameObjX = animator.transform.parent.position.x;

        bool isLeftWallTheMostDistant = Mathf.Abs(leftWallX - gameObjX) >= Mathf.Abs(rightWallX - gameObjX);
        bool isWallCheckTurnedLeft = wallCheck.position.x < gameObjX;

        if (isLeftWallTheMostDistant)
        {
            isGoingLeft = true;
            targetTransform = leftWall;

            if (!isWallCheckTurnedLeft)
            {
                animator.transform.parent.Rotate(0f, 180f, 0f); 
            }
        }
        else
        {
            isGoingLeft = false;
            targetTransform = rightWall;
            if (isWallCheckTurnedLeft)
            {
                animator.transform.parent.Rotate(0f, 180f, 0f); 
            }

        }

        
    }

    void updateTargetTransform(Animator animator) 
    {
        if (isGoingLeft)
        {
            targetTransform = leftWall;
        } else {
            targetTransform = rightWall;
        }
        animator.transform.parent.Rotate(0f, 180f, 0f);    

        animator.SetTrigger("idle");

    }

    
}
