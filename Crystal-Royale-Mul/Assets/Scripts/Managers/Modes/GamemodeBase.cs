using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public abstract class GamemodeBase : MonoBehaviour
{
    public abstract Gamemode GetGamemodeType();

    public abstract bool UsingTeam();
    public abstract void OnSetup();
    public abstract void OnTearDown();

    public abstract bool RoundFinished();

    //public abstract Transform GetSpawnPoint(Team team);

    float m_EndRoundTime;
    float m_LastRealTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GamemodeManager.CurrentGamemode != this)
        {
            return;
        }

        if (RoundFinished() == true)
        {
            Time.timeScale = 0f;

            m_EndRoundTime += (Time.realtimeSinceStartup - m_LastRealTime);

            if (PhotonNetwork.IsMasterClient)
            {
                if (m_EndRoundTime > 2f)
                {

                }
            }
        }
        else
        {
            Time.timeScale = 1f;
            m_EndRoundTime = 0f;
        }

        m_LastRealTime = Time.realtimeSinceStartup;
    }
    protected void SetRoundStartTime()
    {
        ExitGames.Client.Photon.Hashtable newProperties = new ExitGames.Client.Photon.Hashtable();
        newProperties.Add(RoomProperty.StartTime, PhotonNetwork.Time);
        PhotonNetwork.CurrentRoom.SetCustomProperties(newProperties);
    }

    public float GetEndRoundTime()
    {
        return m_EndRoundTime;
    }

    void Awake()
    {
        //PhotonNetwork.automaticallySyncScene = true;
    }
}

