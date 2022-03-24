using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Idle : AnimationBase
{
    private GameObject test;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (GetMonster(animator).target == null)
        //{
        //    animator.SetBool("Run", false);
        //    animator.SetBool("Attack", false);
        //    GetMonster(animator).agent.isStopped = true;
        //    return;
        //}

        //if (GetMonster(animator).target == null)
        //{
        //    GetMonster(animator).FindEnemy();
        //}
        //if (GetMonster(animator).target != null)
        //{
        //    Debug.Log("ok");
        //}
        //if (GetGolem(animator).agent.isStopped)
        //{
        //    GetGolem(animator).agent.isStopped = false;
        //}
        //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        //player = new List<GameObject>();
        //enemy = new List<GameObject>();
        //player.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //foreach (GameObject p in player)
        //{
        //    if(p.GetComponent<PlayerInfo>().team != team)
        //    {
        //        enemy.Add(p);
        //    }
        //}
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if(GetMonster(animator).)
        if (GetMonster(animator).target == null)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            GetMonster(animator).agent.isStopped = true;
            return;
        }
        else if (GetMonster(animator).target != null)
        {
            GetMonster(animator).dis = Vector3.Distance(GetMonster(animator).transform.position, GetMonster(animator).target.transform.position);
            if (GetMonster(animator).target != null && GetMonster(animator).dis > 3f)
            {
                GetMonster(animator).agent.isStopped = false;
                animator.SetBool("Run", true);
            }
            if (GetMonster(animator).target != null && GetMonster(animator).dis <= 3f)
            {
                GetMonster(animator).agent.isStopped = false;
                animator.SetBool("Attack", true);
            }
        }

        //if (golem.GetComponent<Golem>().target != null)
        //{
        //    target = golem.GetComponent<Golem>().target;
        //    animator.SetBool("Run", true);
        //}
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
