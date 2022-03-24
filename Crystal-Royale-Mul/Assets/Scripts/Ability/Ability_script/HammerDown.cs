using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Hammer_Down")]
public class HammerDown : Ability
{
    public GameObject wave;
    GameObject instbullet;
    Transform Hero_transform;

    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero_transform = obj.GetComponent<Transform>();
        Hero.moveable = false;
        Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Ulti", 1);
        //returnLookat();
        instbullet = MasterManager.NetworkInstantiate(wave, Hero_transform.position + (new Vector3(0, -0.9f, 0)), Hero_transform.transform.rotation);
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
