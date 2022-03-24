using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Ability/Big_Slash")]
public class BigslashAbility : Ability
{
    public GameObject wave;
    Transform Hero_transform;
    public bool move;
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero_transform = obj.GetComponent<Transform>();
        move = false;
        Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Ulti", 1);
        returnLookat();
        //GameObject instbullet = Instantiate(wave, Hero_transform.transform.position, Hero_transform.transform.rotation);
        GameObject instbullet = MasterManager.NetworkInstantiate(wave, Hero_transform.transform.position, Hero_transform.transform.rotation);
        //GameObject instbullet = MasterInstantiate(wave, Hero_transform.transform.position, Hero_transform.transform.rotation);
        instbullet.GetComponent<Rigidbody>().AddForce(Hero_transform.transform.forward * 1000);
        Hero.moveable = move;

        // Vector3 targetDirection = Quaternion.AngleAxis(45, Hero_transform.up) * Hero_transform.forward;
        //GameObject instbullet1 = Instantiate(wave, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection));
        //instbullet1.GetComponent<Rigidbody>().AddForce(targetDirection*1000);

        //  Vector3 targetDirection1 = Quaternion.AngleAxis(-45, Hero_transform.up) * Hero_transform.forward;
        // GameObject instbullet2 = Instantiate(wave, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection1));
        // instbullet2.GetComponent<Rigidbody>().AddForce(targetDirection1 * 1000);

        //line.SetPositions(new Vector3[] { Vector3.zero,targetDirection*10 });
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
