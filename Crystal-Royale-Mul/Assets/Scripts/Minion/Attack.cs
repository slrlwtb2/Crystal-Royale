using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AnimationBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (GetMonster(animator).photonView.IsMine)
        //{
        //    GetMonster(animator).Attack();
        //}
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetMonster(animator).target != null)
        {
            //if (GetMonster(animator).photonView.IsMine)
            //{
                GetMonster(animator).dis = Vector3.Distance(GetMonster(animator).transform.position, GetMonster(animator).target.transform.position);
                GetMonster(animator).Attack();
                if (GetMonster(animator).dis > 3)
                {
                    animator.SetBool("Attack", false);
                }
            //}
        }
        else 
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
