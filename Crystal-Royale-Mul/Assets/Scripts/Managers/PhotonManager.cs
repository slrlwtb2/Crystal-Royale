using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //[SerializeField] GameObject main;
    //private static PhotonManager photonManager;
    //[SerializeField] GameObject loading;
    //[SerializeField] GameObject main;
    //private bool inlobby;
    [SerializeField]
    private Text nickname;
    //private static string nick;
    //public int redteam = 0;
    //public int blueteam = 0;
    ///private GameMode gameMode = new GameMode();

    private void Start()
    {
        //photonManager = this;
        //if (inlobby)
        //{
        //    loading.SetActive(false);
        //    main.SetActive(true);
        //}    
        //if (PhotonNetwork.IsConnected)
        //{
        //    PhotonNetwork.JoinRandomRoom();
        //}
        //else
        //{
        //    PhotonNetwork.GameVersion = "0.0.0";
        //    PhotonNetwork.ConnectUsingSettings();
        //}
    }


    //public void Start()
    //{
    //    ConnectToPhoton();
    //    Debug.Log("connected");
    //}

    public void ConnectToPhoton()
    {
        //if (PhotonNetwork.AuthValues == null) return;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickname.text;
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        //PhotonNetwork.NickName = "Crystal "+Random.Range(0,1000).ToString();
        //PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.NetworkingClient.EnableLobbyStatistics = true;
        PhotonNetwork.ConnectUsingSettings();
        //SceneManager.LoadScene("RoomMenu");
        PhotonNetwork.LoadLevel(1);
        //SceneManager.LoadScene("MainMenu");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //public void CreateRoom()
    //{
    //    string roomID = "Room#id:" + Random.Range(0, 10000).ToString() + Random.Range(0, 10000).ToString();
    //    PhotonNetwork.JoinOrCreateRoom(roomID, new RoomOptions { IsOpen = true, MaxPlayers = 4, IsVisible = true }, TypedLobby.Default, null);
    //}

    public override void OnConnected()
    {
        base.OnConnected();
    }
   
    public override void OnConnectedToMaster()
    {
        Debug.Log("We have authed with photon and connected");
        if (!PhotonNetwork.InLobby) {
            //loading.SetActive(true);
            //main.SetActive(false);
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You have joined Lobby");
        //loading.SetActive(false);
        //main.SetActive(true);
        //if (PhotonNetwork.InLobby)
        //{
        //    loading.SetActive(false);
        //    main.SetActive(true);
        //}
    }

    //public void OnEvent(EventData photonEvent)
    //{
    //    if (photonEvent.Code == GameManager.BacktoCurrentRoom)
    //    {
    //        PhotonNetwork.Destroy(GameManager.myPlayer);
    //        PhotonNetwork.LeaveRoom();
    //        //PhotonNetwork.RejoinRoom(PhotonNetwork.CurrentRoom.Name);
    //        PhotonNetwork.LoadLevel(1);
    //    }
    //}

    public override void OnDisconnected(DisconnectCause cause)
    {
        //PhotonNetwork.LeaveRoom();
        //if (photonView.IsMine)
        //{
        //    PhotonNetwork.CleanRpcBufferIfMine(photonView);
        //}
        print("Disconnected from server for reason " + cause.ToString());
    }
    
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }



  
}
