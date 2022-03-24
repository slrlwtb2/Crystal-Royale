using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class selfdes : MonoBehaviourPun
{
    public float activetime;
    // Start is called before the first frame update
    void Start()
    {
        activetime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            activetime -= Time.deltaTime;
            if (activetime <= 0)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
