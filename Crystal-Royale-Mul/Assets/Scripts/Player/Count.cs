using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count
{
    // Start is called before the first frame update
    public List<GameObject> allcam = new List<GameObject>();
    public List<GameObject> allplayer = new List<GameObject>();



    public void AddCam(GameObject cam)
    {
        allcam.Add(cam);
    }

    public void AddPlayer(GameObject pl) 
    {
        allplayer.Add(pl);
    }

}
