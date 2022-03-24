using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Steroid")]
public class SteroidAbility : Ability
{
    float temp;
    public float percentage;
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        temp = Hero.spd;
        Hero.spd = Hero.spd + ((Hero.spd * percentage) / 100);
    }
    public override void BeginCooldown(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero.spd = temp;
    }
}
