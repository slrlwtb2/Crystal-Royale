using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Shooting_Star")]
public class ShootingStarAbility : Ability
{
    public GameObject star;
    public float angles;
    Transform Hero_transform;
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero_transform = obj.GetComponent<Transform>();
        //Hero.moveable = false;
        //Hero.anim.SetFloat("Move", 0);
        returnLookat();
        Hero.anim.SetFloat("Ulti", 1);

        //GameObject instbullet = Instantiate(star, Hero_transform.transform.position, Hero_transform.transform.rotation);
        //instbullet.GetComponent<Rigidbody>().AddForce(Hero_transform.transform.forward * 1000);

        //Vector3 targetDirection = Quaternion.AngleAxis(30, Hero_transform.up) * Hero_transform.forward;
        //GameObject instbullet1 = Instantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection));
        //instbullet1.GetComponent<Rigidbody>().AddForce(targetDirection * 1000);

        //Vector3 targetDirection1 = Quaternion.AngleAxis(-30, Hero_transform.up) * Hero_transform.forward;
        //GameObject instbullet2 = Instantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection1));
        //instbullet2.GetComponent<Rigidbody>().AddForce(targetDirection1 * 1000);

        GameObject instbullet = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Hero_transform.transform.rotation);
        instbullet.GetComponent<Rigidbody>().AddForce(Hero_transform.transform.forward * 1000);

        Vector3 targetDirection = Quaternion.AngleAxis(30, Hero_transform.up) * Hero_transform.forward;
        GameObject instbullet1 = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection));
        instbullet1.GetComponent<Rigidbody>().AddForce(targetDirection * 1000);

        Vector3 targetDirection1 = Quaternion.AngleAxis(-30, Hero_transform.up) * Hero_transform.forward;
        GameObject instbullet2 = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection1));
        instbullet2.GetComponent<Rigidbody>().AddForce(targetDirection1 * 1000);

    }
    public override void BeginCooldown(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Transform Hero_transform = obj.GetComponent<Transform>();

        GameObject instbullet3 = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Hero_transform.transform.rotation);
        instbullet3.GetComponent<Rigidbody>().AddForce(Hero_transform.transform.forward * 1000);

        Vector3 targetDirection3 = Quaternion.AngleAxis(15, Hero_transform.up) * Hero_transform.forward;
        GameObject instbullet4 = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection3));
        instbullet4.GetComponent<Rigidbody>().AddForce(targetDirection3 * 1000);

        Vector3 targetDirection4 = Quaternion.AngleAxis(-15, Hero_transform.up) * Hero_transform.forward;
        GameObject instbullet5 = MasterManager.NetworkInstantiate(star, Hero_transform.transform.position, Quaternion.LookRotation(targetDirection4));
        instbullet5.GetComponent<Rigidbody>().AddForce(targetDirection4 * 1000);
        //Hero.moveable = true;
        //Hero.anim.SetFloat("Ulti", 0);
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
