using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Berserk")]
public class BerserkAbility : Ability
{
    float temp;
    float temp1;
    Transform Hero_transform;
    public float force;
    public override void Activate(GameObject obj)
    {
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        Hero_transform = obj.GetComponent<Transform>();
        temp = Hero.coolDown;
        temp1 = Hero.force;
        Hero.anim.SetFloat("Move", 0);
        //Hero.anim.SetFloat("Attack", 0);
        Hero.anim.SetFloat("Dash", 1);
        Hero.coolDown = 0.2f;
        returnLookat();
        Hero.GetComponent<Rigidbody>().AddForce(Hero.transform.forward * force);
    }

    public override void BeginCooldown(GameObject obj)
    {
        
        Attack_Blade Hero = obj.GetComponent<Attack_Blade>();
        Hero.anim.SetFloat("Dash", 0);
        Hero.coolDown = temp;
        Hero.force = temp1;
    }
    public Vector3 returnLookat()
    {
        Vector3 Lookat = Vector3.zero;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, Hero_transform.transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            Hero_transform.transform.LookAt(hitPoint);
            Lookat = hitPoint;
        }
        return Lookat;
    }
}



