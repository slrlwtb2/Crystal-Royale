using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name + ", " + roomInfo.MaxPlayers;
    }

    public void OnClick_Button()
    {
        //LoadGameSetting(RoomInfo);
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }

    //public void LoadGameSetting(RoomInfo roominfo)
    //{
    //    GameSettings.GameMode = (GameMode)roominfo.CustomProperties["mode"];

    //}
}
