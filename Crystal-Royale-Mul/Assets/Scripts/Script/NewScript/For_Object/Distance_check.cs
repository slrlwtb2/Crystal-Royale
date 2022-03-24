using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Distance_check : MonoBehaviourPunCallbacks
{
    GameObject player;
    public float range;
    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        foreach  (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (pl.GetComponent<PhotonView>().IsMine)
            {
                player = pl;
            }
        }
        if(player == null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void checkDistance(Vector3 a,Vector3 b) 
    {
        if (Vector3.Distance(a, b) > (range)) 
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            checkDistance(gameObject.transform.position, player.transform.position);
        }
    }
}
