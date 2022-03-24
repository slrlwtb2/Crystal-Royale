using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoomMenu : MonoBehaviour
{

    private RoomCanvases roomCanvases;
    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }
    public void  OnClick_LeaveRoom()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team")) 
        {
            string[] custom = {"Team"};
            PhotonNetwork.RemovePlayerCustomProperties(custom);
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            string[] custom = {"Ready"};
            PhotonNetwork.RemovePlayerCustomProperties(custom);
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        {
            string[] custom = {"Character"};
            PhotonNetwork.RemovePlayerCustomProperties(custom);
        }
        //PhotonNetwork.RemovePlayerCustomProperties();
        PhotonNetwork.LeaveRoom(true);
        roomCanvases.CurrentRoom.Hide();
        roomCanvases.CreateOrJoin.Show();
        roomCanvases.heroes.SetActive(false);
    } 
 

}
