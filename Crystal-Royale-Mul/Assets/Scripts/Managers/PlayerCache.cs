using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerCache : MonoBehaviour
{
    public static GameObject myplayer;

    public List<GameObject> playerlist;

    public List<GameObject> teamlist;
    public List<GameObject> enemylist;

    public int team;

    public int cache;


    public GameObject p1;
    public GameObject p2;

    public static PlayerCache instance;
    public bool isp1 = false;
    public bool isp2 = false;
    public static bool stop;



    [SerializeField] GameObject loading;

    private float loadt;

    public void GetPlayer()
    {
        playerlist = new List<GameObject>();

        playerlist.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        foreach (var player in playerlist)
        {
            //Debug.Log(player.GetComponent<PhotonView>()); 
            if (player.GetComponent<PhotonView>().IsMine)
            {
                cache = player.GetComponent<PlayerInfo>().playernocache;
                myplayer = player;
                Debug.Log(cache);
            }
        }
    }

    public void getteammate()
    {
        teamlist = new List<GameObject>();
        foreach (var item in playerlist)
        {
            if (item.GetComponent<PlayerInfo>().team == this.team)
            {
                teamlist.Add(item);
            }

        }
    }

    public void GetEnemy()
    {
        enemylist = new List<GameObject>();
        if (playerlist.Count != 0)
        {
            foreach (var item in playerlist)
            {
                if (item.GetComponent<PlayerInfo>() != null)
                {
                    if (item.GetComponent<PlayerInfo>().team != this.team && item.GetComponent<PlayerInfo>().pno == 1)
                    {
                        enemylist.Add(item);
                        p1 = item;
                    }
                    if (item.GetComponent<PlayerInfo>().team != this.team && item.GetComponent<PlayerInfo>().pno == 2)
                    {
                        enemylist.Add(item);
                        p2 = item;
                    }
                }
            }
        }
    }

    private void SetPlayerNo() {

        if (teamlist.Count == 1)
        {
            isp1 = true;
            //return;
        }
        else
        {
            foreach (GameObject player in teamlist)
            {
                if (player.GetComponent<PhotonView>().IsMine == false)
                {
                    if (cache > player.GetComponent<PlayerInfo>().playernocache)
                    {
                        isp2 = true;
                    }
                    if (cache < player.GetComponent<PlayerInfo>().playernocache)
                    {
                        isp1 = true;
                    }
                }
            }
        }
    }



    private void Awake()
    {
        instance = this;
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        isp1 = false;
        isp2 = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        loadt = 4;
        stop = true;
    }

    // Update is called once per frame
    void Update()
    { 
        if (stop)
        {
            GetPlayer();
            getteammate();
            SetPlayerNo();
            StartCoroutine(waiting());
            Debug.Log("Dont show this");
            loadt -= Time.deltaTime;
            if (loadt >= 0)
            {
                loading.SetActive(true);
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (p.GetComponent<PhotonView>().IsMine)
                    {
                        p.GetComponent<PlayerController3>().spd = 0;
                    }
                }
            }else StartCoroutine(stopupdate());
            //targetingEnemy.SetActive(true);
        }

    }

    IEnumerator stopupdate()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                p.GetComponent<PlayerController3>().spd = p.GetComponent<PlayerController3>().speedcache;
            }
        }
        yield return new WaitForSeconds(5f);
        loading.SetActive(false);
        stop = false;
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2f);
        GetEnemy();
    }

 


}
