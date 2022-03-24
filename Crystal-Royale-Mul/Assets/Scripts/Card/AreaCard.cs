using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AreaCard : MonoBehaviourPun
{

    public Card card;

    private float duration;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        duration = card.duration;
        reset = false;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            duration -= 1 * Time.deltaTime;
            if (duration <= 0)
            {
                reset = true;
                StartCoroutine(wait());
                //PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
