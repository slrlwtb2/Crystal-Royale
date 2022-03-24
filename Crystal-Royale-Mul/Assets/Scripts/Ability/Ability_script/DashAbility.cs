using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class DashAbility : Ability
{

    public float force;
    Transform Hero_transform;
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero_transform = obj.GetComponent<Transform>();
        Rigidbody Hero_rigibody = obj.GetComponent<Rigidbody>();
        Hero.moveable = false;
        returnLookat();
        Hero_rigibody.AddForce(Hero_transform.transform.forward * force);
        Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Ulti", 1);

    }
    public override void BeginCooldown(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero.moveable = true;
        Hero.anim.SetFloat("Ulti", 0);
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
