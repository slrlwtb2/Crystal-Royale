using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MonsterInfo : MonoBehaviourPun
{

    [SerializeField]
    private GameObject fill;

    private int team;
    // Start is called before the first frame update
    void Start()
    {
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        if (photonView.IsMine)
        {
            if (team == 0)
            {
                photonView.RPC("setblue", RpcTarget.All);
            }
            else
            {
                photonView.RPC("setred", RpcTarget.All);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    [PunRPC]
    public void setblue()
    {
        this.fill.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
    }

    [PunRPC]
    public void setred()
    {
        this.fill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }

}
