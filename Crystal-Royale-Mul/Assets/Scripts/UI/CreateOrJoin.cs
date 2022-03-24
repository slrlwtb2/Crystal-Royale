using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoin : MonoBehaviour
{
    [SerializeField]
    private CreateRoom createRoom;
    [SerializeField]
    private RoomList roomList;
    private RoomCanvases roomCanvases;  
    public void FirstInitialize(RoomCanvases canvases){
        roomCanvases = canvases;
        createRoom.FirstInitialize(canvases);
        roomList.FirstInitialize(canvases);
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide(){
        gameObject.SetActive(false);
    }
}
