using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class debug : MonoBehaviour
{
    public void OnClicked()
    {
        Debug.Log(PhotonNetwork.CountOfPlayers.ToString() + " " + PhotonNetwork.CountOfRooms.ToString() + " " + PhotonNetwork.CountOfPlayersOnMaster.ToString());
    }
}
