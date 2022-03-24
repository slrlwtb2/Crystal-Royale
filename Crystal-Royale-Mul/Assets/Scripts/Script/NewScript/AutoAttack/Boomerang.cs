//For monkey character
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Boomerang : MonoBehaviourPun
{

    bool go;//Will Be Used To Change Direction Of Weapon
    //private Transform b;
    //private GameObject nb;

    [SerializeField]
    GameObject player;//Reference To The Main Character

    [SerializeField]
    GameObject playermodel;

    GameObject modelobj;

    [SerializeField]
    GameObject root;

    [SerializeField]
    GameObject weapon_r;

    [SerializeField]
    GameObject sword;//Reference To The Main Character's Weapon

    public int view;

    Transform itemToRotate;//The Weapon That Is A Child Of The Empty Game Object

    Vector3 locationInFrontOfPlayer;//Location In Front Of Player To Travel To


    // Use this for initialization
    void Start()
    {
        if (photonView.IsMine)
        { 
            go = false; //Set To Not Return Yet
            //player = PlayerCache.myplayer;
            foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (pl.GetComponent<PhotonView>().IsMine)
                {
                    this.player = pl;
                    view = this.player.GetComponent<PhotonView>().ViewID;
                }
            }
            this.modelobj = player.transform.Find("model").gameObject;
            this.playermodel = modelobj.transform.Find("Monkey_revamp").gameObject;
            this.root = playermodel.transform.Find("root").gameObject;
            this.weapon_r = root.transform.Find("weaponShield_r").gameObject;
            //if (weapon_r.transform.Find("Banana") != null)
            //{
            //    sword = weapon_r.transform.Find("Banana").gameObject;
            //}
            //else sword = weapon_r.transform.Find("Banana(Clone)").gameObject;
            //b = weapon_r.transform.Find("Banana").gameObject.transform;
            this.sword = weapon_r.transform.Find("Banana").gameObject;
            //player = GameObject.FindGameObjectWithTag("Player");// The GameObject To Return To
            //sword = GameObject.FindGameObjectWithTag("Weapon");//The Weapon The Character Is Holding In The Scene
            //sword = player.transform.Find("Banana");
            //sword.GetComponentInChildren<MeshRenderer>().enabled = false; //Turn Off The Mesh Render To Make The Weapon Invisible
            //PhotonNetwork.Destroy(sword);

            photonView.RPC("togglebanana",RpcTarget.All,false, view);
            

            itemToRotate = gameObject.transform.GetChild(0);

            //Adjust The Location Of The Player Accordingly, Here I Add To The Y position So That The Object Doesn't Go Too Low ...Also Pick A Location In Front Of The Player
            locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) + player.transform.forward * 20f;

            StartCoroutine(Boom());//Now Start The Coroutine
        }
    }

    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(0.5f);//Any Amount Of Time You Want
        go = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine&&this.player!=null)
        {
            itemToRotate.transform.Rotate(0, 0, Time.deltaTime * 1000); //itemToRotate.transform.Rotate(0, Time.deltaTime * 500, 0); //Rotate The Object

            if (go)
            {
                transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 20); //Change The Position To The Location In Front Of The Player            
            }

            if (!go)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), Time.deltaTime * 10); //Return To Player
            }

            if (!go && Vector3.Distance(player.transform.position, transform.position) < 1.5)
            {
                //Once It Is Close To The Player, Make The Player's Normal Weapon Visible, and Destroy The Clone
                //sword.GetComponentInChildren<MeshRenderer>().enabled = true;
                photonView.RPC("togglebanana",RpcTarget.All,true, view);
                //sword.GetComponent<MeshRenderer>().enabled = true;
                //nb = PhotonNetwork.Instantiate("Banana", weapon_r.transform.position, weapon_r.transform.rotation);
                //nb.transform.SetParent(weapon_r.transform);
                //nb.transform.localScale = new Vector3(0.219870001f, 0.219870001f, 0.219870001f);
                //nb.transform.localPosition = new Vector3(-0.0450022146f, -0.0250000004f, -0.0780000016f);
                player.GetComponent<instanceOBJ>().atk = true;
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

    }

    [PunRPC]
    void togglebanana(bool set,int viewid)
    {
         foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
         {
             if (pl.GetComponent<instanceOBJ>()!=null&&pl.GetComponent<PhotonView>().ViewID == viewid)
             {
                pl.GetComponent<instanceOBJ>().banana.GetComponent<MeshRenderer>().enabled = set;
             }
         }
    }

       // void togglebanana(bool set,int viewid)
   // {
       // if (photonView.IsMine)
        //{
            //player = PlayerCache.myplayer;
          //  foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
          //  {
          //      if (pl.GetComponent<instanceOBJ>()!=null&&pl.GetComponent<PhotonView>().ViewID == viewid)
          //      {
          //          pl.GetComponent<instanceOBJ>().banana.GetComponent<MeshRenderer>().enabled = set;
                //pl.GetComponent<Boomerang>().player = pl;
                //pl.GetComponent<Boomerang>().modelobj = pl.GetComponent<Boomerang>().player.transform.Find("model").gameObject;
                //pl.GetComponent<Boomerang>().playermodel = pl.GetComponent<Boomerang>().modelobj.transform.Find("Monkey_revamp").gameObject;
                //pl.GetComponent<Boomerang>().root = pl.GetComponent<Boomerang>().playermodel.transform.Find("root").gameObject;
                //pl.GetComponent<Boomerang>().weapon_r = pl.GetComponent<Boomerang>().root.transform.Find("weaponShield_r").gameObject;
                //pl.GetComponent<Boomerang>().sword = pl.GetComponent<Boomerang>().weapon_r.transform.Find("Banana").gameObject;
                    //b.GetComponent<Boomerang>().sword.GetComponent<MeshRenderer>().enabled = set;
            //    }
         //   }
       
        //}
   // }
  


}