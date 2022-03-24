using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Berserk_2nd")]
public class Berserk_2nd : Ability
{
    public override void Activate(GameObject obj)
    {
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Dash", 1);

    }

    public override void BeginCooldown(GameObject obj)
    {
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        Hero.anim.SetFloat("Dash", 0);
    }
}



