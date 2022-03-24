using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Timer : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    bool startTimer = false;
    double timeDecrementValue;
    double startTime;
    [SerializeField] double timer = 607;
    [SerializeField] Text time;

    ExitGames.Client.Photon.Hashtable CustomValue;

    private void Awake()
    {
        //PhotonNetwork.AddCallbackTarget(this);
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    CustomValue = new ExitGames.Client.Photon.Hashtable();
        //    startTime = PhotonNetwork.Time;
        //    startTimer = true;
        //    CustomValue.Add("StartTime", startTime);
        //    PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
        //}
    }
    void Start()
    {
        
        //if (PhotonNetwork.IsMasterClient)
        //{
            CustomValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomValue);
            //OnRoomPropertiesUpdate(CustomValue);
        //}

        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    StartCoroutine(wait());
        //    //startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        //    //startTimer = true;
        //}
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;
        timeDecrementValue = timer - (PhotonNetwork.Time - startTime);
        time.text = timeDecrementValue.ToString("0");
        //if (timeDecrementValue >= timer)
        //{
        //    GameManager.instance.timeout = true;
        //}
        if (timeDecrementValue <= 0)
        {
            GameManager.instance.timeout = true;
            startTimer = false;
        }
    }

    //public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        object propsTimes = propertiesThatChanged["StartTime"];
    //        startTime = double.Parse(propsTimes.ToString());
    //        //startTime = (double)propsTimes;
    //        startTimer = true;
    //    }

    //}

    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(1);
    //    startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
    //    startTimer = true;
    //}





}
