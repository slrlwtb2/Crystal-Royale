using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack_Blade : MonoBehaviourPun
{
    [SerializeField]
    GameObject hitbox;
    [SerializeField]
    public GameObject spinHitbox;

    float lastshot;
    public float coolDown;
    public Animator anim;
    public float force = 500f;
    PlayerController3 player;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (anim == null)
            {
                anim = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
                player = gameObject.GetComponent<PlayerController3>();
            }
            if (Input.GetKeyUp("space"))
            {
                player.moveable = false;
                Debug.Log("Click");
                if (Time.time - lastshot < coolDown)
                {

                    Debug.Log("On cool down");
                }
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run_Blade"))
                {
                    player.moveable = false;
                    anim.SetFloat("Attack", 1);
                    returnLookat();
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                }
                else
                {
                    player.moveable = false;
                    lastshot = Time.time;
                    anim.SetFloat("Attack", 1);
                    returnLookat();
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                }
            }
            else { anim.SetFloat("Attack", 0); }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
            {
                player.moveable = false;
                hitbox.SetActive(true);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Blade"))
            {
                player.moveable = true;
                hitbox.SetActive(false);
            }
        }
        //else { hitbox.SetActive(false);  }
    }
    public Vector3 returnLookat()
    {
        Vector3 Lookat = Vector3.zero;
        
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, gameObject.transform.position);
        
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            gameObject.transform.LookAt(hitPoint);
            Lookat = hitPoint;
        }
        return Lookat;
    }

    
}
