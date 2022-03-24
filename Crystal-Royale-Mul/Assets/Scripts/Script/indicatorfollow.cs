using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class indicatorfollow : MonoBehaviourPun
{
    GameObject indicator;
    void Start()
    {
        //indicator = GameObject.Find("indicator_fire");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (indicator == null)
            {
                indicator = GameObject.Find("indicator_fire");
                if(indicator!=null) indicator.SetActive(false);
            }

            indicator.transform.position = gameObject.transform.position;

            if (Input.GetKeyDown("space"))
            {
                indicator.SetActive(true);
            }
            else
            {
                if (Input.GetKeyUp("space"))
                {
                    indicator.SetActive(false);
                }
            }
        }
    }
}
