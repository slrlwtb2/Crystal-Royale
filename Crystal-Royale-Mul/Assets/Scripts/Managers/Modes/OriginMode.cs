using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OriginMode : GamemodeBase
{

    public const float RoundTime = 10 * 60.0f;

    //public Transform redspawm;

    //public Transform bluespawn;

    //public Transform redCrytal;

    //public Transform blueCrystal;

    public override Gamemode GetGamemodeType()
    {
        return Gamemode.Origin;
    }

    //public override Transform GetSpawnPoint(Team team)
    //{
    //    if (team == Team.Blue)
    //    {
    //        return bluespawn;
    //    }
    //    return redspawm;
    //}

    public override void OnSetup()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            SetRoundStartTime();
        }
    }



    public override void OnTearDown()
    {
        throw new System.NotImplementedException();
    }

    public override bool RoundFinished()
    {
        throw new System.NotImplementedException();
    }

    public override bool UsingTeam()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

   
    
}
