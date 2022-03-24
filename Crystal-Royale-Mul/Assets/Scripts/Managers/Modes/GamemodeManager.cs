using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum Gamemode
{
    Origin,
    //Phantom,
    //Shuffle,
    Count
}

public class GamemodeManager : MonoBehaviour
{
    public static GamemodeManager Instance;

    public Gamemode SelectedGamemode;

    GamemodeBase[] m_Gamemodes;

   
    public static GamemodeBase CurrentGamemode
    {
        get
        {
            if (Instance == null)
            {
                return null;
            }
            return Instance.GetCurrentGamemode();
        }

    }

    private void Awake()
    {
        Instance = this;

        FindGamemodes();


    }

    void FindGamemodes()
    {
        m_Gamemodes = new GamemodeBase[(int)Gamemode.Count];
        m_Gamemodes[0] = GetComponent<OriginMode>();
        //m_Gamemodes[1] = GetComponent<>();
        //m_Gamemodes[2] = GetComponent<>();
    }

    void FindCurrentMapMode()
    {
        if (!PhotonNetwork.InRoom) return;

    }

    void InitiateSelectedGamemode()
    {
        for (int i = 0; i < m_Gamemodes.Length; i++)
        {
            if (i == (int)SelectedGamemode)
            {
                m_Gamemodes[i].OnSetup();
            }
            else
            {
                m_Gamemodes[i].OnTearDown();
            }
        }
    }

    public GamemodeBase GetGamemode(Gamemode mode)
    {
        return m_Gamemodes[(int)mode];
    }

    public GamemodeBase GetCurrentGamemode()
    {
        return GetGamemode(SelectedGamemode);
    }
}
