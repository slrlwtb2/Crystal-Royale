using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{

    Animator animator;//You may not need an animator, but if so declare it here    

    int noOfClicks; //Determines Which Animation Will Play
    bool canClick; //Locks ability to click during animation event
    public PlayerController3 player;
    void Start()
    {
        //Initialize appropriate components 
        animator = GetComponent<Animator>();
        player = gameObject.GetComponent<PlayerController3>();
        noOfClicks = 0;
        canClick = true;
    }

    void Update()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
            
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ComboStarter();

        }
    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }

        if (noOfClicks == 1)
        {
            animator.SetInteger("animation", 1);
        }

   
    }

    public void ComboCheck()
    {

        canClick = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") && noOfClicks == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            animator.SetInteger("animation", 0);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") && noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo           
            animator.SetInteger("animation", 2);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle          
            animator.SetInteger("animation", 0);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo          
            animator.SetInteger("animation", 3);
            canClick = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        { //Since this is the third and last animation, return to idle           
            animator.SetInteger("animation", 0);
            noOfClicks = 0;
            canClick = true;
        }
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        //{
        //    animator.SetInteger("animation", 0);
        //    noOfClicks = 0;
        //    canClick = true;
        //}
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Paladin")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_Sword"))
        {
            animator.SetInteger("animation", 0);
            noOfClicks = 0;
            canClick = true;
        }
    }
}