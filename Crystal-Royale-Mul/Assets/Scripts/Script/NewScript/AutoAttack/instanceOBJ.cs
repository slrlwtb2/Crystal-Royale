using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class instanceOBJ : MonoBehaviourPun
{
   // [SerializeField]
    public GameObject modelobj,model, root, weapon_r, banana;
    private Transform b;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    public GameObject boomer;
    public GameObject gun;
    public float coolDown;
    float lastshot;
    public Animator anim;
    public bool atk;
    // Use this for initialization
    void Start()
    {
        atk = true;
        this.modelobj = this.transform.Find("model").gameObject;
        model = modelobj.transform.Find("Monkey_revamp").gameObject;
        root = model.transform.Find("root").gameObject;
        weapon_r = root.transform.Find("weaponShield_r").gameObject;
        banana = weapon_r.transform.Find("Banana").gameObject;
        b = banana.transform;
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
                if (!atk)
                {
                    Debug.Log("On cool down");
                    
                }
                else
                {
                    audioSource.PlayOneShot(clip);
                    atk = false;
                    anim.SetFloat("Attack", 1);
                    returnLookat();
                    GameObject clone;
                    //clone = Instantiate(boomer, gun.transform.position, transform.rotation) as GameObject;
                    clone = MasterManager.NetworkInstantiate(boomer, gun.transform.position, transform.rotation);
                    //PhotonNetwork.Destroy(banana);
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

   
    
}

