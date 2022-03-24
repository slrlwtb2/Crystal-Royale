using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Crystal : MonoBehaviourPunCallbacks,IDamageable,IPunObservable
{
    [SerializeField] private Slider healthbar;
    //[SerializeField] private GameObject tower;
    [SerializeField] private GameObject al;

    public float health;
    private int team;
    private bool getattack;


    void Awake()
    {
        if (gameObject.tag == "RedCrystal") team = 1;
        if (gameObject.tag == "BlueCrystal") team = 0;
        //if (photonView.IsMine)
        //{
            health = 1000;
            //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
            healthbar.maxValue = health;
            healthbar.minValue = 0;
            healthbar.value = 1000;
        //}

    }
    // Start is called before the first frame update
    void Start()
    {
        getattack = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (getattack == false)
        //{
        //    if (this.gameObject.tag == "RedCrystal")
        //    {
        //        foreach (Player pl in PhotonNetwork.PlayerList)
        //        {
        //            if ((int)pl.CustomProperties["Team"] == 1)
        //            {

        //                photonView.RPC("unalert", pl);

        //            }
        //        }
        //    }
        //    if (this.gameObject.tag == "BlueCrystal")
        //    {
        //        foreach (Player pl in PhotonNetwork.PlayerList)
        //        {
        //            if ((int)pl.CustomProperties["Team"] == 0)
        //            {

        //                photonView.RPC("unalert", pl);

        //            }
        //        }
        //    }
        //}
    }

    //private void OnTriggerEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        TakeDamage(100);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Bullet")
        //{
        //    TakeDamage(100);
        //}
    }


    public void TakeDamage(float damage)
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
  
        //health -= damage;
        //healthbar.value = health;
    }

    public void damage(float damage, string cname)
    {
        getattack = true;
        if (cname == "RedCrystal")
        {
            foreach (Player pl in PhotonNetwork.PlayerList)
            {
                if ((int)pl.CustomProperties["Team"] == 1)
                {
                    //if (getattack)
                    //{
                        photonView.RPC("alert", pl);
                        StartCoroutine(wait2(pl));
                    //}
                }
            }
        }
        if (cname == "BlueCrystal")
        {
            foreach (Player pl in PhotonNetwork.PlayerList)
            {
                if ((int)pl.CustomProperties["Team"] == 0)
                {
                    //if (getattack)
                    //{
                        photonView.RPC("alert", pl);
                        StartCoroutine(wait2(pl));
                    //}
                }
            }
        }
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage, cname);

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(healthbar.value);
            stream.SendNext(team);
        }
        if (stream.IsReading)
        {
            health = (float)stream.ReceiveNext();
            healthbar.value = (float)stream.ReceiveNext();
            team = (int)stream.ReceiveNext();
            
        }
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage,string c)
    {
        if (c == "RedCrystal")
        {
            GameObject.FindGameObjectWithTag("RedCrystal").GetComponent<Crystal>().health -= damage;
            GameObject.FindGameObjectWithTag("RedCrystal").GetComponent<Crystal>().healthbar.value = GameObject.FindGameObjectWithTag("RedCrystal").GetComponent<Crystal>().health;
            //GameObject.FindGameObjectWithTag("RedCrystal").GetComponent<Crystal>().getattack = false;
            //StartCoroutine(wait(c));
            //health -= damage;
            //healthbar.value = health;
        }
        if (c == "BlueCrystal")
        {
            GameObject.FindGameObjectWithTag("BlueCrystal").GetComponent<Crystal>().health -= damage;
            GameObject.FindGameObjectWithTag("BlueCrystal").GetComponent<Crystal>().healthbar.value = GameObject.FindGameObjectWithTag("BlueCrystal").GetComponent<Crystal>().health;
            //GameObject.FindGameObjectWithTag("BlueCrystal").GetComponent<Crystal>().getattack = false;
            //StartCoroutine(wait(c));
        }
    }

    [PunRPC]
    public void alert()
    {
        //GameObject banner = new GameObject();
        //banner = GameObject.FindGameObjectWithTag("alert");
        //banner.transform.Find("Text").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("alert").transform.Find("Text").gameObject.SetActive(true);
    }

    [PunRPC]
    public void unalert()
    {
        //GameObject banner = new GameObject();
        //banner = GameObject.FindGameObjectWithTag("alert");
        //banner.transform.Find("Text").gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("alert").transform.Find("Text").gameObject.SetActive(false);
    }

    IEnumerator wait2(Player pl)
    {
        yield return new WaitForSeconds(1);
        photonView.RPC("unalert", pl);
    }

    IEnumerator wait(string cry)
    {
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag(cry).GetComponent<Crystal>().getattack = false;
    }
    //[PunRPC]
    //public void unalert(int team)
    //{
    //    foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
    //    {
    //        if (item.gameObject.GetComponent<PlayerInfo>().team == team)
    //        {

    //        }
    //    }
    //}
}
