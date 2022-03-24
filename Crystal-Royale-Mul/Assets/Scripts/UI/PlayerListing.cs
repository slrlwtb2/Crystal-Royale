using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text text;

    public Player Player{get; private set;}
    public bool Ready = false;
    private string ready;
    private void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);   
    }

    public void SetPlayerInfo(Player player){
        Player = player;
        SetPlayerText(player);
    }
    
    private void SetPlayerText(Player player)
    {
        Player = player;
        int result = -1;
        int team = -1;
        string character = "";
        //int cache = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        string t ="";
        if(player.CustomProperties.ContainsKey("RandomNumber")) result = (int)player.CustomProperties["RandomNumber"];
        //text.text = result.ToString() + ", "+ player.NickName;
        if (player.CustomProperties.ContainsKey("Team")) 
        {
            team = (int)player.CustomProperties["Team"];
            if (team == 0) t = "Blue";
            else t = "Red";
        }
        if (!player.IsMasterClient)
        {
            if (Ready == true) ready = "R";
            else ready = "UN";
        }
        if (player.CustomProperties.ContainsKey("Character")) {
            character = (string)player.CustomProperties["Character"];
        }
        if (!player.IsMasterClient)
        {
            text.text = player.NickName + " " + t + " " + ready + " " + character;
        }
        else text.text = player.NickName + " " + t + " " + character;
        //text.text = player.UserId;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer != null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey("Ready"))
            {
                SetPlayerText(targetPlayer);
            }
            if (changedProps.ContainsKey("Team"))
            {
                SetPlayerText(targetPlayer);
            }
            if (changedProps.ContainsKey("Character"))
            {
                SetPlayerText(targetPlayer);
            }
        }
    }
}
