using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Auto_Atatck/Bow")]
public class Attack_Bow : Auto_attack
{
    [SerializeField]
    private GameObject bullet;
    GameObject instbullet;
    
    public override void Activate(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        GameObject player = GameObject.FindWithTag("Player");
        Hero.anim.SetFloat("Move", 0);
        Hero.anim.SetFloat("Attack", 1);
        instbullet = Instantiate(bullet, player.transform.position, player.transform.rotation);
        instbullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * 1500);
        
        
    }
    public override void BeginCooldown(GameObject obj)
    {
        PlayerController3 Hero = obj.GetComponent<PlayerController3>();
        Hero.anim.SetFloat("Attack", 0);
    }
}
