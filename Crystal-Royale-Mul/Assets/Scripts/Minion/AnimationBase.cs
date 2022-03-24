using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : StateMachineBehaviour
{
    private Monster monster;

    public Monster GetMonster(Animator animator)
    {
        if (monster == null)
        {
            monster = animator.GetComponentInParent<Monster>();
            if (monster == null)
            {
                monster = animator.GetComponent<Monster>();
            }
        }
        return monster;
    }
}
