using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class InstantiateExample : MonoBehaviourPun
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private GameObject bl;

    private GameObject[] cam;

    private GameObject[] play;

    private GameObject pl;

    private GameObject scam;

    

    private void Awake()
    {
        pl = MasterManager.NetworkInstantiate(_prefab, transform.position, Quaternion.identity);
        // scam = MasterManager.NetworkInstantiate(bl, transform.position, Quaternion.identity);
        //scam = MasterManager.NetworkInstantiate(vcam, Camera.main.transform.position, Quaternion.identity);
         
        //AtPlayerSpawn();
    }

    private void Update()
    { 
        //if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length <= 4)
        //{
        //    cam = GameObject.FindGameObjectsWithTag("Vcam");
        //    play = GameObject.FindGameObjectsWithTag("Player");
        //    Countcam();
        //}
        //else return;
        //Debug.Log(cam.Length.ToString() +" "+ play.Length.ToString());
    }

   // private void Countcam() 
   // {
        //if (PhotonNetwork.PlayerList.Length != 4)
        //{
            //foreach (GameObject p in play)
            //{
            //    foreach (GameObject c in cam)
            //    {
            //        if (c.GetComponent<PhotonView>().Owner.UserId == p.GetComponent<PhotonView>().Owner.UserId)
            //        {
            //            c.GetComponent<CinemachineVirtualCamera>().LookAt = p.transform;
            //            c.GetComponent<CinemachineVirtualCamera>().Follow = p.transform;
            //        }
            //       // else continue;
            //    }
            //}
       // }
        //else return;
   // }


    //private void AtPlayerSpawn()
    //{  
    //    if(pl.GetComponent<PhotonView>().Owner.UserId == scam.GetComponent<PhotonView>().Owner.UserId) 
    //    {
    //        scam.GetComponent<CinemachineVirtualCamera>().LookAt = pl.transform;
    //        scam.GetComponent<CinemachineVirtualCamera>().Follow = pl.transform;
    //    }    
    //}
}
