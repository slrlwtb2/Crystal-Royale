using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField]
    GameObject redPlayerPrefab;
    [SerializeField]
    GameObject bluePlayerPrefab;

    private string[] custom = {"Team","Ready","Character"};

    private float wait;
    //[SerializeField]
    public GameObject redCrystal;
    //[SerializeField]
    public GameObject blueCrystal;

    [SerializeField]
    private List<GameObject> enemyplayer;
    //[SerializeField]
    //private List<GameObject> playerlist = new List<GameObject>();
    //[SerializeField]
    //private List<GameObject> teamlist = new List<GameObject>();

    private Dictionary<int, GameObject> enemy;
    private Dictionary<int, GameObject> ourteam;


    //private bool stop = false;
    private int cache;

    public static GameObject rc;
    public static GameObject bc;


    public static GameObject myPlayer;
    public static GameManager instance;
    //
    public int reds=3000;
    public int blues=3000;
    //
    public static bool isp1 = false;
    public static bool isp2 = false;


    public bool timeout;

    public int p1 = 1;
    public int p2 = 2;
 
    //private int[] p = { 1,2};

    public GameObject target;

    public Text score;
    public GameObject messageText;
    public Button pause;
    public static readonly byte BacktoCurrentRoom =1;
    private int team;

    //public GameObject pauseCanvas;
    //public bool paused = false;
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected) PhotonNetwork.OfflineMode = true;
        instance = this;
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        SetScore();
        if (!PhotonNetwork.IsMasterClient) wait = 8;
        //enemyplayer = new List<GameObject>();
        //playerlist = new List<GameObject>();
        //enemy = new Dictionary<int, GameObject>();
    }

    private void Start()
    {
        PlayerDetermine();
        timeout = false;
        //set paused state
        //SetPaused();
        //messageText.SetActive(false);
        //check that we dont have a local instance before we instantiate the prefab
        if (NetworkPlayerManager.localPlayerInstance == null)
        {
        
            //instantiate the correct player based on the team
            //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
            Debug.Log($"Team number {team} is being instantiated");
            //instantiate the blue player if team is 0 and red if it is not
            if (team == 0)
            {
                //get a spawn for the correct team
                Transform spawn = SpawnManager.instance.GetTeamSpawn(0);
                
                myPlayer = PhotonNetwork.Instantiate(bluePlayerPrefab.name, spawn.position, spawn.rotation);
           
                
            }
            else
            {
                //now for the red team
                Transform spawn = SpawnManager.instance.GetTeamSpawn(1);
                
                myPlayer = PhotonNetwork.Instantiate(redPlayerPrefab.name, spawn.position, spawn.rotation);
         
            }

            if (PhotonNetwork.IsMasterClient)
            {
                Transform csb = SpawnManager.instance.GetCrystalSpawn(0);
                bc = PhotonNetwork.InstantiateRoomObject(blueCrystal.name, csb.position, csb.rotation);
                //bc = PhotonNetwork.Instantiate(blueCrystal.name, csb.position, csb.rotation);
                Transform csr = SpawnManager.instance.GetCrystalSpawn(1);
                //rc = PhotonNetwork.Instantiate(redCrystal.name, csr.position, csr.rotation);
                rc = PhotonNetwork.InstantiateRoomObject(redCrystal.name, csr.position, csr.rotation);
            }

            if (!PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(w());
                //bc = GameObject.FindGameObjectWithTag("BlueCrystal");
                //rc = GameObject.FindGameObjectWithTag("RedCrystal");
            }

        }
        //foreach (var item in PhotonNetwork.PlayerList)
        //{
        //    Debug.Log(item.GetPlayerNumber());
            
        //}
        //SetPlayerNo();

    }
   


    private void Update()
    {
        SetScore();
        wait -= Time.deltaTime;
        //SetPlayerNo();
        //FindEnemy();
        if (PhotonNetwork.IsMasterClient)
        {
            if (bc.GetComponent<Crystal>().health <= 0)
            {
                StartCoroutine(DisplayMessage("Red Wins"));
                if (PhotonNetwork.IsMasterClient)
                {
                    StartCoroutine(backtoroom());
                }
            }
            if (rc.GetComponent<Crystal>().health <= 0)
            {
                StartCoroutine(DisplayMessage("Blue Wins"));

                if (PhotonNetwork.IsMasterClient)
                {
                    StartCoroutine(backtoroom());
                }
            }
        }
        if (!PhotonNetwork.IsMasterClient&&wait<=0)
        {
            if (bc.GetComponent<Crystal>().health <= 0)
            {
                StartCoroutine(DisplayMessage("Red Wins"));
                    StartCoroutine(backtoroom());
                
            }
            if (rc.GetComponent<Crystal>().health <= 0)
            {
                StartCoroutine(DisplayMessage("Blue Wins"));
                    StartCoroutine(backtoroom());
                
            }
        }

        if (timeout)
        {
            if (reds > blues)
            {
                StartCoroutine(DisplayMessage("Red Wins"));
                StartCoroutine(backtoroom());
            }
            if (blues > reds)
            {
                StartCoroutine(DisplayMessage("Blue Wins"));
                StartCoroutine(backtoroom());
            }
            if (blues == reds)
            {
                if(rc.GetComponent<Crystal>().health > bc.GetComponent<Crystal>().health)
                {
                    StartCoroutine(DisplayMessage("Red Wins"));
                    StartCoroutine(backtoroom());
                }
                if (rc.GetComponent<Crystal>().health < bc.GetComponent<Crystal>().health)
                {
                    StartCoroutine(DisplayMessage("Blue Wins"));
                    StartCoroutine(backtoroom());
                }
                else
                {
                    StartCoroutine(DisplayMessage("Draw"));
                    StartCoroutine(backtoroom());
                }
            }
        }

        // else return;
    }

    private void PlayerDetermine()
    {
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"]==0)
        { 
           bluePlayerPrefab = Resources.Load("Player_"+(string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]) as GameObject;  
            //if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "Paladin")
            //{
            //    bluePlayerPrefab = Resources.Load("Player_Paladin") as GameObject;
            //}
            //if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "Sky")
            //{
            //    bluePlayerPrefab = Resources.Load("Player_Sky") as GameObject;
            //}
            //if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "Assasin")
            //{
            //    bluePlayerPrefab = Resources.Load("Player_Blade") as GameObject;
            //}
            //if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "Knight")
            //{
            //    bluePlayerPrefab = Resources.Load("Player_Sword") as GameObject;
            //}
            //if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "Fire")
            //{
            //    bluePlayerPrefab = Resources.Load("Player_Fire") as GameObject;
            //}
        }
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 1)
        {
            redPlayerPrefab = Resources.Load("Player_" + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]) as GameObject;
        }
    }
    
    private void FindEnemy()
    {
        //enemyplayer.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //if (enemy.Count ==0)
        //{
        //    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Player"))
        //    {
        //        if (e.GetComponent<PlayerInfo>().team != this.team)
        //        {
        //            enemy.Add(1, e);
        //        }
        //    }
        //    return;
        //    //currentState = MinionStates.Idle;
        //    //foreach (GameObject player in playerlist)
        //    //{
        //    //    if (player.GetComponent<PlayerInfo>().team != this.team)
        //    //    {
        //    //        Debug.Log(player.GetComponent<PlayerInfo>().team);
        //    //        enemyplayer.Add(player);
        //    //    }
        //    //}
        //    //if (enemyplayer.Count == 0) return;
        //}
    }

    public void SetScore()
    {
        //bscore.text = blues.ToString();
        //rscore.text = reds.ToString();
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] == 0)
            {
                score.text = blues.ToString();
            }
            else
            {
                score.text = reds.ToString();
            }
        }
        
    }

    public void SetPlayerNo()
    {
        //playerlist.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //foreach (Player item in PhotonNetwork.PlayerList)
        //{
        //foreach (GameObject player in playerlist)
        //{
        //    //if (player.GetComponent<PhotonView>().IsMine)
        //    //{
        //    //    cache = player.GetComponent<PlayerInfo>().playernocache;
        //    //    Debug.Log(cache);
        //    //}
        //  if (player.GetComponent<PlayerInfo>().team == this.team)
        //   {
        //        teamlist.Add(player);
        //        if (player.GetComponent<PhotonView>().IsMine)
        //        {
        //            cache = player.GetComponent<PlayerInfo>().playernocache;
        //            Debug.Log(cache);
        //        }
        //    }
        //}

        //if (teamlist.Count == 1)
        //{
        //    isp1 = true;
        //}
        //else
        //{
        //foreach (GameObject player in teamlist)
        //{
        //    if (player.GetComponent<PhotonView>().IsMine == false)
        //    {
        //        if (cache > player.GetComponent<PlayerInfo>().playernocache)
        //        {
        //            isp2 = true;
        //        }
        //        if (cache < player.GetComponent<PlayerInfo>().playernocache)
        //        {
        //            isp1 = true;
        //        }
        //    }
        //}
        //}

        //}
        //playerlist.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //for (int i = 0; i < playerlist.Count; i++)
        //{
        //    if(playerlist[i].GetComponent<PlayerInfo>().team == this.team)
        //    {
        //        //PhotonNetwork.pl
        //    }
        //    playerlist[i].GetComponent<PlayerInfo>().playerno = p[i];
        //}
    }

    public void OnClick_Quit()
    {
        PhotonNetwork.Destroy(myPlayer);
        PhotonNetwork.RemovePlayerCustomProperties(custom);
        PhotonNetwork.LoadLevel(1);
        
        PhotonNetwork.LeaveRoom();
       
        //Quit();
    }

    
    public void Quit()
    {
        //Destroy(myPlayer);
        PhotonNetwork.Destroy(myPlayer);
        //PhotonNetwork.OpRemoveCompleteCache();
        //PhotonNetwork.LeaveRoom();
        PhotonNetwork.RemovePlayerCustomProperties(custom);
        PhotonNetwork.LoadLevel(1);
        //PhotonNetwork.LeaveLobby();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //if (photonView.IsMine == true && otherPlayer.IsLocal)
        //{
        //    PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        //}
    }

    public override void OnLeftRoom()
    {
        //PhotonNetwork.LoadLevel(1);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == GameManager.BacktoCurrentRoom)
        {
            Quit();
            //PhotonNetwork.RejoinRoom(PhotonNetwork.CurrentRoom.Name);

        }
    }

    public void SetPlayer()
    {

    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public int GetScore(int team)
    {
        if (team == 0)
        {
            return this.blues;
        }
        else return this.reds;
    }

    IEnumerator DisplayMessage(string message)
    {
        messageText.SetActive(true);
        messageText.GetComponentInChildren<Text>().text = message;
        //messageText.text = message;
        yield return new WaitForSeconds(10);
        //messageText.text = "";
        messageText.SetActive(false);
        messageText.GetComponentInChildren<Text>().text = "";
    }

    
    IEnumerator backtoroom()
    {
        yield return new WaitForSeconds(4);
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(BacktoCurrentRoom, null, raiseEventOptions, sendOptions);
    }

    IEnumerator w()
    {
        yield return new WaitForSeconds(2);
        bc = GameObject.FindGameObjectWithTag("BlueCrystal");
        rc = GameObject.FindGameObjectWithTag("RedCrystal");
    }


}