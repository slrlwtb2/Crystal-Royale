using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

//public class MapData
//{
//    public string name;
//    public int scene;
//}

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    [SerializeField]
    private Dropdown _gameMode;

    [SerializeField]
    private Dropdown _map;

    double startTime;
    
    private RoomCanvases roomCanvases;  
    public void FirstInitialize(RoomCanvases canvases){
        roomCanvases = canvases;
    }



    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.MaxPlayers = 4;
        options.IsVisible = true;
        options.BroadcastPropsChangeToAll = true;
        options.CleanupCacheOnLeave = true;
        //options.CustomRoomPropertiesForLobby = new string[] { "mode" };

        //ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        //startTime = PhotonNetwork.Time;
        //properties.Add("StartTime", startTime);
        //properties.Add("mode", (int)GameSettings.GameMode);

        //options.CustomRoomProperties = properties;
 
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options,TypedLobby.Default, null);
      
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully");
        //roomCanvases.CreateOrJoin.Hide();
        //roomCanvases.CurrentRoom.Show();
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed "+message);
    }
    

    public void ChangeMode()
    {
        //int newMode = (int)GameSettings.GameMode;
        //if (newMode >= System.Enum.GetValues(typeof(GameMode)).Length) newMode = 0;
        //GameSettings.GameMode = (GameMode)newMode;
    }

    public void ChangeMap()
    {
      
    }

    public void Log_out()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    } 
        
   
}
