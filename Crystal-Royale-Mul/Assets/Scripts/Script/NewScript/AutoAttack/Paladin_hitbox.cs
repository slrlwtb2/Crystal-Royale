using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin_hitbox : MonoBehaviour
{

    Animator animator;//You may not need an animator, but if so declare it here    
    public PlayerController3 player;
    public GameObject hitbox1;
    public GameObject hitbox2;
    public GameObject hitbox3;
    void Start()
    {
        //Initialize appropriate components 
        animator = GetComponent<Animator>();
        player = gameObject.GetComponent<PlayerController3>();
        hitbox1.SetActive(false);
        hitbox2.SetActive(false);
        hitbox3.SetActive(false);
    }

    void Update()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            GetComponent<PlayerController3>().changeable = false;
            hitbox1.SetActive(true);
            //player.moveable = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            GetComponent<PlayerController3>().changeable = false;
            hitbox1.SetActive(false);
            hitbox2.SetActive(true);
            //player.moveable = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            GetComponent<PlayerController3>().changeable = false;
            hitbox2.SetActive(false);
            hitbox3.SetActive(true);
            player.moveable = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Paladin"))
        {
            GetComponent<PlayerController3>().changeable = true;
            hitbox3.SetActive(false);
            hitbox2.SetActive(false);
            hitbox1.SetActive(false);
            player.moveable = true;
        }
        //if (animator == null)
        //{
        //    animator = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
        //}
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        //{
        //    GetComponent<PlayerController3>().changeable = false;
        //    hitbox1.SetActive(true);
        //}
        //else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        //{
        //    GetComponent<PlayerController3>().changeable = false;
        //    hitbox2.SetActive(true);
        //}
        //else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        //{
        //    GetComponent<PlayerController3>().changeable = false;
        //    hitbox3.SetActive(true);
        //}
        //else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Paladin"))
        //{
        //    GetComponent<PlayerController3>().changeable = true;
        //    hitbox3.SetActive(false);
        //    hitbox2.SetActive(false);
        //    hitbox1.SetActive(false);
        //}

    }

}