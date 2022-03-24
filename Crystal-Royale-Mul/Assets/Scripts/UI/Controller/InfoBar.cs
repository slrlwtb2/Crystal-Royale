using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class InfoBar : MonoBehaviourPun
{

    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject text2;

    [SerializeField]
    private GameObject fill;

    private int team;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            StartCoroutine(wait());
            //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
            //num = GetComponentInParent<PlayerInfo>().pno;
            ////else
            ////{
            //if (team == 0)
            //{
            //    //photonView.RPC("setblue", RpcTarget.All);
            //    foreach (Player item in PhotonNetwork.PlayerListOthers)
            //    {
            //        if ((int)item.CustomProperties["Team"] != team)
            //        {
            //            //photonView.RPC("flip", item);
            //            photonView.RPC("setblue", item,this.num);
            //        }
            //        if ((int)item.CustomProperties["Team"] == team)
            //        {
            //            photonView.RPC("setblue2", item,this.num);
            //        }
            //    }
            //    this.text.SetActive(false);
            //    this.text2.SetActive(true);
            //    this.text2.GetComponent<Text>().text = "B" + this.num;
            //    //this.text2.transform.rotation = new Quaternion(0, -180, 0, 0);
            //}
            //else
            //{
            //    //photonView.RPC("setred", RpcTarget.All);
            //    foreach (Player item in PhotonNetwork.PlayerListOthers)
            //    {
            //        if ((int)item.CustomProperties["Team"] != team)
            //        {
            //            //photonView.RPC("flip", item);
            //            photonView.RPC("setred", item,this.num);
            //        }
            //        if ((int)item.CustomProperties["Team"] == team)
            //        {
            //            photonView.RPC("setred2", item,this.num);
            //        }
            //    }
            //    this.text.SetActive(true);
            //    this.text2.SetActive(false);
            //    this.text2.GetComponent<Text>().text = "R" + this.num;
            //    this.text.transform.rotation = new Quaternion(0, 0, 0, 0);
            //}
            //}
        }
        //if (team == 1)
        //{
        //    foreach (Player item in PhotonNetwork.PlayerListOthers)
        //    {
        //        if ((int)item.CustomProperties["Team"] != team)
        //        {
        //            photonView.RPC("flip2", item);
        //        }
        //    }

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [PunRPC]
    public void setblue(int n)
    {
        this.text.SetActive(false);
        this.text2.SetActive(true);
        this.text2.GetComponent<Text>().text = "B" + n;
        this.text2.transform.rotation = new Quaternion(0, 0, 0, 0);
        this.fill.GetComponent<Image>().color = new Color32(0,0,255,255);
    }

    [PunRPC]
    public void setblue2(int n)
    {
        this.text.SetActive(false);
        this.text2.SetActive(true);
        this.text2.GetComponent<Text>().text = "B" + n;
        this.text2.transform.rotation = new Quaternion(0, -180, 0, 0);
        this.fill.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
    }


    [PunRPC]
    public void setred(int n)
    {
        this.text.SetActive(true);
        this.text2.SetActive(false);
        this.text.GetComponent<Text>().text = "R" + n;
        this.text.transform.rotation = new Quaternion(0, -180, 0, 0);
        this.fill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }

    [PunRPC]
    public void setred2(int n)
    {
        this.text.SetActive(true);
        this.text2.SetActive(false);
        this.text.GetComponent<Text>().text = "R" + n;
        this.text.transform.rotation = new Quaternion(0, 0, 0, 0);
        this.fill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        num = GetComponentInParent<PlayerInfo>().pno;
        //else
        //{
        if (team == 0)
        {
            //photonView.RPC("setblue", RpcTarget.All);
            foreach (Player item in PhotonNetwork.PlayerListOthers)
            {
                if ((int)item.CustomProperties["Team"] != team)
                {
                    //photonView.RPC("flip", item);
                    photonView.RPC("setblue", item, this.num);
                }
                if ((int)item.CustomProperties["Team"] == team)
                {
                    photonView.RPC("setblue2", item, this.num);
                }
            }
            this.text.SetActive(false);
            this.text2.SetActive(true);
            this.text2.GetComponent<Text>().text = "B" + this.num;
            this.fill.GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            //this.text2.transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        else
        {
            //photonView.RPC("setred", RpcTarget.All);
            foreach (Player item in PhotonNetwork.PlayerListOthers)
            {
                if ((int)item.CustomProperties["Team"] != team)
                {
                    //photonView.RPC("flip", item);
                    photonView.RPC("setred", item, this.num);
                }
                if ((int)item.CustomProperties["Team"] == team)
                {
                    photonView.RPC("setred2", item, this.num);
                }
            }
            this.text.SetActive(true);
            this.text2.SetActive(false);
            this.text.GetComponent<Text>().text = "R" + this.num;
            this.text.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.fill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(num);
        }
        if (stream.IsReading)
        {
            num = (int)stream.ReceiveNext();
        }
    }
    //[PunRPC]
    //public void flip2()
    //{
    //    this.text.transform.rotation = new Quaternion(0, 180, 0, 0);
    //}



}
