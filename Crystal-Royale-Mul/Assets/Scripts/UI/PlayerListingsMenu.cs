using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;


public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    public enum Team
    {
        Red,
        Blue
    };

    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing playerListing;
    [SerializeField]
    private Text readyUpText;

    private int cr;

    public List<Toggle> selectplayer;

    public static Toggle player1;
    public static Toggle player2;

    [SerializeField]
    private Button rbutton;

    [SerializeField]
    private Button sbutton;

    [SerializeField]
    private Button redb;

    [SerializeReference]
    private Button blued;

    [SerializeField]
    private Button sh;

    

    private bool ready = false;

    public static PlayerListingsMenu instance;
    public ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    [SerializeField]
    private GameObject heroes;

    private RoomCanvases roomCanvases;
    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    private List<PlayerListing> list = new List<PlayerListing>();

    // private void Awake(){
    //     GetCurrentRoomPlayer();
    // }
    private void Awake()
    {
        //instance = this;
        cr = 0;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetReady(false);
        GetCurrentRoomPlayer();
        if (PhotonNetwork.IsMasterClient) 
        {
            rbutton.gameObject.SetActive(false);
            sbutton.gameObject.SetActive(false);
        }
        if (!PhotonNetwork.IsMasterClient) 
        {
            sbutton.gameObject.SetActive(false);
            rbutton.gameObject.SetActive(false);
        } 
        
    }
    private void Update()
    {
        //start when everyone ready is possible to do in this section
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["Team"] != null && PhotonNetwork.LocalPlayer.CustomProperties["Character"] != null)
            {
                cr = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Player != PhotonNetwork.LocalPlayer)
                    {
                        if (!list[i].Ready)
                        {
                            cr--;
                            //cr = false;
                            //return;
                        }
                        if (list[i].Ready)
                        {
                            cr++;
                            //cr = true;
                        }
                    }
                }
                if(cr==list.Count-1)
                {
                    Debug.Log(cr);
                    sbutton.gameObject.SetActive(true);
                    sh.gameObject.SetActive(false);
                    redb.gameObject.SetActive(false);
                    blued.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log(cr+"pls");
                    sbutton.gameObject.SetActive(false);
                    sh.gameObject.SetActive(true);
                    redb.gameObject.SetActive(true);
                    blued.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["Team"] != null && PhotonNetwork.LocalPlayer.CustomProperties["Character"]!=null)
            {
                rbutton.gameObject.SetActive(true);
            }
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for(int i=0;i< list.Count;i++){
            Destroy(list[i].gameObject);
        }
        list.Clear();
    }

    public void SelectPlayer()
    {
        //foreach (var item in selectplayer)
        //{
        //if (item.isOn)
        //{
        customProperties["Character"] = heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name;
        //customProperties["Character"] = item.name;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
        // }
        //}


    }


    private void SetReady(bool state)
    {
        ready = state;
        if(ready)
        {
            readyUpText.text = "CANCEL";
            sh.gameObject.SetActive(false);
            redb.gameObject.SetActive(false);
            blued.gameObject.SetActive(false);
        }
        else
        {
            readyUpText.text = "READY";
            sh.gameObject.SetActive(true);
            redb.gameObject.SetActive(true);
            blued.gameObject.SetActive(true);
        }
    }

    private void GetCurrentRoomPlayer()
    {
        if(!PhotonNetwork.IsConnected) return;
        if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }   
    }

    // public override void OnLeftRoom()
    // {
    //     _content.DestroyChildren();
    // }

    private void AddPlayerListing(Player player){
        int index = list.FindIndex(x=>x.Player == player);
        if(index != -1){
           list[index].SetPlayerInfo(player);
       }
       else{
            PlayerListing listing = Instantiate(playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                list.Add(listing);
            }
       }
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = list.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(list[index].gameObject);
            list.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if(list[i].Player != PhotonNetwork.LocalPlayer)
            //    {
            //        if(!list[i].Ready)
            //        {
            //            return;
            //        }
            //    }    
            //}

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(2);
        }
    }

    public void OnClick_ready()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            SetReady(!ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All,PhotonNetwork.LocalPlayer,ready);
            customProperties["Ready"] = !ready;
            PhotonNetwork.SetPlayerCustomProperties(customProperties);
            //base.photonView.RpcSecure("ChangeReadyState", RpcTarget.MasterClient,true,PhotonNetwork.LocalPlayer,ready);
            //PhotonNetwork.RemoveRPCs();
        }
    }

    public void Click_Blue()
    {
        JoinTeam(0);
    }

    public void Click_Red()
    {
        JoinTeam(1);
    }

    //private void SelectPlayer()
    //{
    //    if (player1.isOn)
    //    {
           
    //    }
    //    if (player2.isOn)
    //    {

    //    }
    //}

    public void JoinTeam(int team)
    {
        customProperties["Team"] = team;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
        //if (team == 0)
        //{
        //    PhotonNetwork.LocalPlayer.JoinTeam((byte)Team.Blue);
        //}
        //if(team == 1)
        //{
        //    PhotonNetwork.LocalPlayer.JoinTeam((byte)Team.Red);
        //}
        //do we already have a team?
        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        //{
        //    //we already have a team- so switch teams
        //    PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        //}
        //else
        //{
        //    //we dont have a team yet- create the custom property and set it
        //    //0 for blue, 1 for red
        //    //set the player properties of this client to the team they clicked
        //    ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable { { "Team", team } };
        //    //set the property of Team to the value the user wants
        //    PhotonNetwork.SetPlayerCustomProperties(playerProps);
        //}

        //join the random room and launch game- the GameManager will spawn the correct model in based on the property
        //PhotonNetwork.JoinRandomRoom();
    }

 

    [PunRPC]
    private void RPC_ChangeReadyState(Player player,bool ready)
    {
        int index = list.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            list[index].Ready = ready;
            
            // Destroy(list[index].gameObject);
            // list.RemoveAt(index);
        }
    }
}
