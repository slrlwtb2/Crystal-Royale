using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

//public enum Team
//{
//    Red,
//    Blue,
//    None,
//}

public class PlayerInfo : MonoBehaviourPun, IPunObservable
{ 

    public int team;
    public int playernocache;
    public int pno;
    public bool getkill;

    public bool isdead;
    //public static PlayerInfo localPlayer;
    //public bool IsLocalPlayer = true;
    private float deadcd;
    public float current_health;

    public GameObject respawning;
    public GameObject ress;
    //Team m_team;
    //public Team Team
    //{
    //    get
    //    {
    //        return m_team;
    //    }
    //}

    public PlayerInfo(bool t)
    {
        //this.awayteam = t;
    }
    private void Awake()
    {
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        playernocache = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        current_health = 100;
        getkill = false;
        //foreach (var item in PhotonNetwork.PlayerList)
        //{
        //    if (item.IsLocal)
        //    {
        //        playernocache = item.GetPlayerNumber();
        //        Debug.Log(item.GetPlayerNumber());
        //    }
        //}
        isdead = false;
        //Debug.Log(playernocache);
    }

    // Start is called before the first frame update
    void Start()
    {
        respawning = GameObject.FindGameObjectWithTag("Respawn");
        respawning.transform.Find("Respanel").gameObject.SetActive(false);
        //deadcd = 10;
        //if (GameManager.isp1==true) pno = 1;
        //if (GameManager.isp2==true) pno = 2;
        //Debug.Log(pno);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {

            if (PlayerCache.instance.isp1 == true)
            {
                pno = 1;
                //PlayerCache.stop = false;
                //PlayerCache.stop = false;
                //stop = false;
                //Debug.Log("pls");
            }
            if (PlayerCache.instance.isp2 == true)
            {
                pno = 2;
                //PlayerCache.stop = false;
                //PlayerCache.stop = false;
                //stop = false;
                //Debug.Log("pls");
            }
            //Debug.Log(pno);
            if (current_health <= 0 && getkill)
            {
                
                
                //this.GetComponent<PlayerController3>().anim.SetBool("Die", true);
                //this.GetComponent<PlayerController3>().healthbar.value = current_health;
                var emission = this.GetComponentInChildren<ParticleSystem>().emission;
                emission.enabled = false;
                if (isdead)
                {
                   
                    isdead = false;
                    photonView.RPC("PlayerKilled", RpcTarget.All, team);
                    this.GetComponent<PlayerController3>().anim.SetBool("Die", true);
                    if (this.GetComponent<Sword_hitbox>() != null || this.GetComponent<Paladin_hitbox>() != null)
                    {
                        Debug.Log("yesy");
                        this.GetComponent<PlayerController3>().anim.SetInteger("animation", 0);
                    }
                    this.GetComponent<PlayerController3>().enabled = false;
                    StartCoroutine(dead());
                    //this.GetComponent<PlayerController3>().anim.SetBool("Die", true);
                    
                    //StartCoroutine(dead());

                }

                //this.GetComponent<PlayerController3>().enabled = false;
                //photonView.RPC("SetRenderers", RpcTarget.All, false);
                //this.transform.position = new Vector3(0, -10, 0);
                //Transform spawn = SpawnManager.instance.GetTeamSpawn(team);
                //transform.position = spawn.position;
                //transform.rotation = spawn.rotation;
                respawning.transform.Find("Respanel").gameObject.SetActive(true);
                deadcd = 10;
                //respawning.SetActive(true);
                
            }

            if (deadcd >= 0)
            {
                deadcd -= Time.deltaTime;
                respawning.transform.Find("Respanel").gameObject.GetComponentInChildren<Text>().text = deadcd.ToString("0");
                //this.GetComponent<PlayerController3>().anim.SetBool("Die", true);
                //respawning.GetComponentInChildren<Text>().text = deadcd.ToString("0");
            }
            else
            {
                //respawning.SetActive(false);
                if (respawning.transform.Find("Respanel").gameObject != null)
                {
                    respawning.transform.Find("Respanel").gameObject.SetActive(false);
                }
                //else 

                GetComponent<PlayerController3>().enabled = true;
               
                getkill = false;
                photonView.RPC("SetRenderers", RpcTarget.All, true);
            }




        }
    }



    //IEnumerator Respawn()
    //{
    //    if (photonView.IsMine)
    //    {
    //        //SetRenderers(false);
    //        float refill = 100;
    //        photonView.RPC("SetHealth", RpcTarget.All, refill);
    //        //this.GetComponent<PlayerController3>().healthbar.value = current_health;
    //        var emission = this.GetComponentInChildren<ParticleSystem>().emission;
    //        emission.enabled = false;
    //        photonView.RPC("SetRenderers", RpcTarget.All, false);
    //        this.GetComponent<PlayerController3>().enabled = false;
    //        this.transform.position = new Vector3(0, -10, 0);
    //        Transform spawn = SpawnManager.instance.GetTeamSpawn(team);
    //        transform.position = spawn.position;
    //        transform.rotation = spawn.rotation;
    //        yield return new WaitForSeconds(10);
    //        GetComponent<PlayerController3>().enabled = true;
    //        photonView.RPC("SetRenderers", RpcTarget.All, true);
    //        //********//
    //        //PlayerCache.stop = true;
    //        //********//
    //    }
    //    //SetRenderers(true);
    //}

    [PunRPC]
    void PlayerKilled(int team)
    {
        if (team == 0)
        {
            GameManager.instance.reds += 150;
        }
        else
        {
            GameManager.instance.blues += 150;
        }
    }


    [PunRPC]
    public void SetRenderers(bool set)
    {
        //if (photonView.IsMine)
        //{
        this.gameObject.transform.Find("model").gameObject.SetActive(set);
        this.gameObject.transform.Find("HealthFloat").gameObject.SetActive(set);
        //this.gameObject.SetActive(set);
        //}
    }
    [PunRPC]
    public void SetHealth(float h)
    {
        this.current_health = h;
        this.GetComponent<PlayerController3>().healthbar.value = current_health;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(team);
            stream.SendNext(playernocache);
            stream.SendNext(pno);
            stream.SendNext(current_health);
            stream.SendNext(getkill);
            stream.SendNext(isdead);
  
            //stream.SendNext(this.transform.position);
        }
        else
        {
            //we are reading
            team = (int)stream.ReceiveNext();
            playernocache = (int)stream.ReceiveNext();
            pno = (int)stream.ReceiveNext();
            current_health = (float)stream.ReceiveNext();
            getkill = (bool)stream.ReceiveNext();
            isdead = (bool)stream.ReceiveNext();
        }
    }
    
    IEnumerator dead()
    {
       // isdead = false;
        yield return new WaitForSeconds(1);
        this.GetComponent<PlayerController3>().anim.SetBool("Die", false);
        //if (this.GetComponent<Sword_hitbox>() != null || this.GetComponent<Paladin_hitbox>() != null)
        //{
        //    Debug.Log("yesy");
        //    this.GetComponent<PlayerController3>().anim.SetInteger("animation", 0);
        //}
        photonView.RPC("SetRenderers", RpcTarget.All, false);
        GetComponent<PlayerController3>().spd = GetComponent<PlayerController3>().speedcache;
        float refill = 100;
        photonView.RPC("SetHealth", RpcTarget.All, refill);
        this.transform.position = new Vector3(0, -10, 0);
        Transform spawn = SpawnManager.instance.GetTeamSpawn(team);
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
    }

    //[PunRPC]
    //void OnRespawn(Vector3 spawnPosition, Quaternion spawnRotation)
    //{

    //}

    //public void SetTeam(Team team)
    //{
    //    m_team = team; 
    //}

    //public void Damage(float damage)
    //{
    //    max_health -= damage;
    //}

    //public void Damage(float damage, PlayerInfo attacker)
    //{
    //    max_health -= damage;
    //}

    //void OnDamage(PlayerInfo attacker)
    //{
    //    if(health <= 0)
    //    {
    //        health = 0;
    //        Invoke("Respawn", 10f);
    //    }
    //    //Set dieing Animation


}

