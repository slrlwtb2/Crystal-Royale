using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FlameBall : MonoBehaviourPun
{
    public GameObject falme_ground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                PhotonNetwork.Destroy(this.gameObject);
                GameObject Flame = MasterManager.NetworkInstantiate(falme_ground, gameObject.transform.position + (new Vector3(0, 0, 0)), Quaternion.identity);
            }
        }
    }
}
