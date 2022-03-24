using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Spin")]
public class Spin : Ability
{

    public override void Activate(GameObject obj)
    {
        
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        PlayerController3 move = obj.GetComponent<PlayerController3>();
        //move.moveable = false;
        Hero.spinHitbox.SetActive(true);
        //Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Ulti", 1);

    }

    public override void BeginCooldown(GameObject obj)
    {

        PlayerController3 move = obj.GetComponent<PlayerController3>();
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        move.moveable = true;
        Hero.spinHitbox.SetActive(false);
        Hero.anim.SetFloat("Ulti", 0);
    }
}
