using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    //the instnce on the scene
    public static SpawnManager instance;
    //the team spawn
    GameObject[] redTeamSpawns;
    GameObject[] blueTeamSpawns;

    GameObject redCrystal;
    GameObject blueCrystal;

    // Start is called before the first frame update
    void Awake()
    {
        //recreate the instance on awake- ie if scene reloads
        instance = this;
        redTeamSpawns = GameObject.FindGameObjectsWithTag("RedSpawn");
        blueTeamSpawns = GameObject.FindGameObjectsWithTag("BlueSpawn");
        if (PhotonNetwork.IsMasterClient)
        {
            redCrystal = GameObject.FindGameObjectWithTag("RedCSpawn");
            blueCrystal = GameObject.FindGameObjectWithTag("BlueCSpawn");
        }
    }

    public Transform GetRandomRedSpawn()
    {
        //return a transform for one of the red spawns
        return redTeamSpawns[Random.Range(0, redTeamSpawns.Length)].transform;
    }

    public Transform GetRandomBlueSpawn()
    {
        //return a transform for one of the red spawns
        return blueTeamSpawns[Random.Range(0, blueTeamSpawns.Length)].transform;
    }
    //this method gets given the team number to find a spawn for
    public Transform GetTeamSpawn(int teamNumber)
    {
        return teamNumber == 0 ? GetRandomBlueSpawn() : GetRandomRedSpawn();
    }

    public Transform GetRandomBlueCrySpawn()
    {
        //return a transform for one of the red spawns
        return blueCrystal.transform;
    }
    public Transform GetRandomRedCrySpawn()
    {
        //return a transform for one of the red spawns
        return redCrystal.transform;
    }

    public Transform GetCrystalSpawn(int teamNumber)
    {
        return teamNumber == 0 ? GetRandomBlueCrySpawn() : GetRandomRedCrySpawn();
    }
}
