using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TargetingEnemy : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playercache;
    //private List<GameObject> enemylist;

    [HideInInspector]
   // public static TargetingEnemy instance;

    private int team;
    [SerializeField]
    private Toggle p1;
    [SerializeField]
    private Toggle p2;
    [SerializeField]
    private Toggle tur;

    private bool stop = true;

    //private GameObject enemyp1;
    //private GameObject enemyp2;

    public GameObject target;
    
    private void Awake()
    {
        //instance = this;
        //target = new GameObject();
        //enemyp1 = new GameObject();
        //enemyp2 = new GameObject();
        p1.isOn = false;
        p2.isOn = false;
        tur.isOn = false;
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
    }
    // Start is called before the first frame update
    void Start()
    {
        //FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //FindEnemy();
        if(p1.isOn&&playercache.GetComponent<PlayerCache>().p1!=null&& playercache.GetComponent<PlayerCache>().p1.GetComponent<PlayerInfo>().getkill != true)
        {
            target = playercache.GetComponent<PlayerCache>().p1; 
        }
        if (p1.isOn && playercache.GetComponent<PlayerCache>().p1 != null && playercache.GetComponent<PlayerCache>().p1.GetComponent<PlayerInfo>().getkill == true)
        {
            target = null;
        }
        if (p2.isOn && playercache.GetComponent<PlayerCache>().p2 != null && playercache.GetComponent<PlayerCache>().p2.GetComponent<PlayerInfo>().getkill!=true)
        {
            target = playercache.GetComponent<PlayerCache>().p2;
        }
        if (p2.isOn && playercache.GetComponent<PlayerCache>().p2 != null && playercache.GetComponent<PlayerCache>().p2.GetComponent<PlayerInfo>().getkill== true)
        {
            target = null;
        }
        if (tur.isOn)
        {
            if (team == 0)
            {
                //target = GameManager.rc;
                target = GameObject.FindGameObjectWithTag("RedCrystal");
            }
            else 
            {
                target = GameObject.FindGameObjectWithTag("BlueCrystal");
                //target = GameManager.bc; 
            }
        }

        if (!p1.isOn && !p2.isOn && !tur.isOn)
        {
            target = null;
        }
    

        //Debug.Log(target);

        // foreach (var item in enemyplayer)
        // {
        //     if (item.GetComponent<PlayerInfo>().pno == 1)
        //     {
        //         p1.gameObject.SetActive(true);
        //     }
        //     if(item.GetComponent<PlayerInfo>().pno == 2)
        //     {
        //         p2.gameObject.SetActive(true);
        //     }
        // }
    }

    void FindEnemy()
    {
        //if (stop)
        //{
        //    foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        //    {
        //        if (item.GetComponent<PlayerInfo>().team != this.team)
        //        {
        //            enemyplayer.Add(item);
        //        }
        //    }
        //    stop = false;
        //}
        //foreach (var item in PlayerCache.instance.enemylist)
        //{
        //    if(item.GetComponent<PlayerInfo>().pno==1){
        //        enemyp1 = item;
        //    }
        //    if(item.GetComponent<PlayerInfo>().pno==2){
        //        enemyp2 = item;
        //    }
        //}
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2f);
    }

  
}
