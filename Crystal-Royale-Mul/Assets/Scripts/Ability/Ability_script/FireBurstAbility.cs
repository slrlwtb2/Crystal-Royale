using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/FireBurst")]
public class FireBurstAbility : Ability
{
    float temp;
    public override void Activate(GameObject obj)
    {
        Rangeattack Hero = obj.GetComponent<Rangeattack>();
        temp = Hero.coolDown;
        Hero.coolDown = 0.2f;

    }


    public override void BeginCooldown(GameObject obj)
    {
        Rangeattack Hero = obj.GetComponent<Rangeattack>();
        Hero.coolDown = temp;

    }
    

}
