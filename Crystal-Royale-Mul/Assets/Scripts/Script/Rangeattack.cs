using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Rangeattack : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject gun;
    GameObject player;
    GameObject instbullet;
    float lastshot;
    public float coolDown;
    Vector3 lookat;
    public Animator anim;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    public float force = 2000f;
    void Start()
    {
        player = gameObject;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (anim == null)
            {
                anim = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
            }
            if (Input.GetKeyUp("space"))
            {
                Debug.Log("Click");
                if (Time.time - lastshot < coolDown)
                {
                    Debug.Log("On cool down");
                }
                else
                {
                    audioSource.PlayOneShot(clip);
                    lastshot = Time.time;
                    anim.SetFloat("Attack", 1);
                    returnLookat();
                    instbullet = MasterManager.NetworkInstantiate(bullet, gun.transform.position, gun.transform.rotation);
                    instbullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                }
            }
            else { anim.SetFloat("Attack", 0); }
        }
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
    //[SerializeField] 
    //GameObject bullet;

    //[SerializeField]
    //GameObject gun;

    //[SerializeField]
    //GameObject player;

    //[SerializeField]
    //GameObject mc;

    //GameObject instbullet;
    //PhotonView pv;
    //Animator anim;


    //public float force = 2000f;


    //private void Awake()
    //{
    //    pv = this.GetComponent<PhotonView>();
    //    anim = mc.GetComponent<Animator>();
    //}

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (pv.IsMine)
    //    {
    //        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0))
    //        {
    //            anim.SetFloat("Attack", 1);
    //            pv.RPC("Shoot", RpcTarget.All);
    //        }
    //        else anim.SetFloat("Attack", 0) ;
    //    }    
    //}

    //[PunRPC]
    //private void Shoot() 
    //{
    //    if (pv.IsMine)
    //    {
    //        instbullet = MasterManager.NetworkInstantiate(bullet, gun.transform.position, player.transform.rotation);
    //        instbullet.transform.Rotate(Vector3.left * 90);
    //        instbullet.GetComponent<Rigidbody>().AddForce(transform.forward * force);
    //        Destroy(instbullet, 0.5f);
    //    }
    //}


}
