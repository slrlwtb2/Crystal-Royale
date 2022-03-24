using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private Room _room;

    private List<Room> list = new List<Room>();
    
    private RoomCanvases roomCanvases;

    public void FirstInitialize(RoomCanvases canvases)
    {
        roomCanvases = canvases;
    }

    public override void OnJoinedRoom()
    {
        roomCanvases.CurrentRoom.Show();
        roomCanvases.CreateOrJoin.Hide();
        roomCanvases.heroes.SetActive(true);
        _content.DestroyChildren();
        list.Clear();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo inf in roomList)
        {
            if (inf.RemovedFromList)
            {
                int index = list.FindIndex(x => x.RoomInfo.Name == inf.Name);
                if (index != -1)
                {
                    Destroy(list[index].gameObject);
                    list.RemoveAt(index);
                }
            }
            else
            {
                int index = list.FindIndex(x=>x.RoomInfo.Name == inf.Name);
                if(index == -1)
                {
                    Room listing = Instantiate(_room, _content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(inf);
                        list.Add(listing);
                    }
                }
                else
                {

                }
            }

        }
    }
}
