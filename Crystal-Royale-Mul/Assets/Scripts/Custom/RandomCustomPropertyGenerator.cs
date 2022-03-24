using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private void SetCustomNumber()
    {
        System.Random ran = new System.Random();
        int result = ran.Next(0,99);
        text.text = result.ToString();

        customProperties["RandomNumber"] = result;
        //PhotonNetwork.LocalPlayer.CustomProperties = customProperties;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    public void OnClick_Button()
    {
        SetCustomNumber();
    }

}
