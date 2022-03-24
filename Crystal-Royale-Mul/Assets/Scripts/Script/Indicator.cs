using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Indicator : MonoBehaviour
{
    //https://www.youtube.com/watch?v=Hi61o1_duwo
    // Start is called before the first frame update
    [SerializeField]
    Vector3 origin;
    Vector3 endPoint;
    Vector3 mousePos;
    LineRenderer indicator;
    PhotonView pv;

    Material mat;

    RaycastHit hit;
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
        indicator = this.GetComponent<LineRenderer>();
        indicator.startWidth = 0.5f;
        indicator.endWidth = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetMouseButton(0) || Input.GetKey("space"))
            {
                DrawLine();
            }
            else { indicator.enabled = false; }
        }
    }

    public void DrawLine() 
    {
        //Draw Line indicator   
        origin = this.transform.position + (this.transform.forward * 0.6f)+(this.transform.up*-0.8f);
        endPoint = origin + this.transform.forward * 9f;

        indicator.SetPosition(0, origin);
        indicator.SetPosition(1,endPoint);

        indicator.enabled = true;
    }
}
