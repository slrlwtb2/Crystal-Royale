using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Run : AnimationBase
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GetMonster(animator).target != null)
        GetMonster(animator).agent.SetDestination(GetMonster(animator).target.transform.position);
        //idle.golem.GetComponent<Golem>().agent.SetDestination(idle.golem.GetComponent<Golem>().target.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GetMonster(animator).target != null)
        {
            GetMonster(animator).dis = Vector3.Distance(GetMonster(animator).transform.position, GetMonster(animator).target.transform.position);
            GetMonster(animator).agent.SetDestination(GetMonster(animator).target.transform.position);
            //distance = Vector3.Distance(idle.golem.GetComponent<Golem>().target.transform.position, idle.golem.transform.position);
            //idle.golem.GetComponent<Golem>().agent.SetDestination(idle.golem.GetComponent<Golem>().target.transform.position);
            if (GetMonster(animator).dis <= 3f)
            {
                GetMonster(animator).agent.isStopped = true;
                //animator.SetBool("Run", false);
                //idle.golem.GetComponent<Golem>().agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
            if (GetMonster(animator).dis > 3f && GetMonster(animator).target != null)
            {
                GetMonster(animator).agent.SetDestination(GetMonster(animator).target.transform.position);
                //animator.SetBool("Run", true);
            }
        }
        else animator.SetBool("Run", false);
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
