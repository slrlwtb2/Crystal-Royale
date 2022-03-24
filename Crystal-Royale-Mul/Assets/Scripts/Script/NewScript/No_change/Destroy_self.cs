//For destroy it self
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destroy_self : MonoBehaviourPun
{
    void Update()
    {
        if (photonView.IsMine)
        {
            StartCoroutine(wait());
            //Destroy(gameObject, 3f);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.Destroy(gameObject);

    }
}
