using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Grass : MonoBehaviourPun
{
    public string number;
    string layer;
    Camera cam;
    int oldMask;

    void Start()
    {
       
            cam = Camera.main;
            layer = ("Grass" + number);
            oldMask = cam.cullingMask;
        
        //this.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
            //if (other.CompareTag("Player"))
            //{
            //    other.gameObject.layer = LayerMask.NameToLayer(layer);
            // photonView.RPC("ChangeLayersRecursively", RpcTarget.All, other.transform.position,other.transform.rotation,layer);
            //    ChangeLayersRecursively(other.transform, layer);
            //}
            //if (other.gameObject.layer == LayerMask.NameToLayer(layer))
            //{
                //var r = this.gameObject.GetComponent<Renderer>().material.color;
                //r.a = 0.5f;
            
                cam.cullingMask = LayerMask.GetMask(layer, "Default", "UI");
            //}
       
    }

    private void OnTriggerExit(Collider other)
    {
      
            if (other.CompareTag("Player"))
            {
                //other.gameObject.layer = LayerMask.NameToLayer("Default");
                //ChangeLayersRecursively(other.transform, "Default");
                //photonView.RPC("ChangeLayersRecursively", RpcTarget.All, layer);
                cam.cullingMask = oldMask;
            }
        
    }


    [PunRPC]
    public void ChangeLayersRecursively(Transform trans, string name)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.layer = LayerMask.NameToLayer(name);
            ChangeLayersRecursively(child, name);
        }
    }
}
