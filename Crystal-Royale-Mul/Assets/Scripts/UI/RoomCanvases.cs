using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomCanvases : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    private CreateOrJoin createOrJoin;

    public CreateOrJoin CreateOrJoin{get {return createOrJoin;}}

    [SerializeField]
    private CurrentRoom currentRoom;
    public CurrentRoom CurrentRoom{get {return currentRoom;}}

   
    public GameObject heroes;

    private void Awake()
    {
        FirstInitialize();
    }

    

    private void FirstInitialize()
    {
        CreateOrJoin.FirstInitialize(this);
        CurrentRoom.FirstInitialize(this);
        if (PhotonNetwork.InRoom) {
            CreateOrJoin.Hide();
            CurrentRoom.Show();
           // Debug.Log("show?");
            heroes.SetActive(true);
        }
        
    }

    
}
