using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Jump")]
public class JumpAbility : Ability
{
    float temp;
    public float force;
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Transform Hero_tranform = obj.GetComponent<Transform>();
        Rigidbody Hero_rigibody = obj.GetComponent<Rigidbody>();
        temp = Hero.spd;
        //Hero.spd = Hero.spd + ((Hero.spd * 50) / 100);
        Hero_rigibody.AddForce(Hero_tranform.transform.up * force);

    }
    public override void BeginCooldown(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero.spd = temp;
    }
}
