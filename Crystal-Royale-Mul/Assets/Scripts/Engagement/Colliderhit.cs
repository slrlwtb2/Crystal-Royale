using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Colliderhit : MonoBehaviourPun,IPunObservable
{
    public int team;
    //[SerializeField]
    public float damage;
    [SerializeField]
    private bool attur;
    // Start is called before the first frame update

    private void Awake()
    {
        //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
    }
    void Start()
    {
        //team = GetComponentInParent<PlayerInfo>().team;
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("work");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Monster" && other.gameObject.GetComponent<Monster>().team != this.team)
            {
                other.gameObject.GetComponent<Monster>().TakeDamage(damage);
            }
            if (other.gameObject.tag == "Player")
            {
                if (other.GetComponent<PlayerInfo>().team == team)
                {
                    return;
                }
                else
                {
                    if (this.gameObject.tag == "destroy")
                    {
                        other.GetComponent<PlayerController3>().TakeDamage(damage);
                        PhotonNetwork.Destroy(this.gameObject);
                    }
                    else
                    {
                        other.GetComponent<PlayerController3>().TakeDamage(damage);
                    }
                }
            }
            if(other.gameObject.tag=="RedCrystal"||other.gameObject.tag=="BlueCrystal")
            {
                //StartCoroutine(wait());
                if (attur)
                {
                    if (other.gameObject.tag == "RedCrystal" && team != 1)
                    {
                        other.gameObject.GetComponent<Crystal>().damage(damage,other.gameObject.tag);
                        if (this.gameObject.tag == "Bullet")
                        {
                            PhotonNetwork.Destroy(this.gameObject);
                        }
                        //PhotonNetwork.Destroy(this.gameObject);
                    }
                    if (other.gameObject.tag == "BlueCrystal" && team != 0)
                    {
                        other.gameObject.GetComponent<Crystal>().damage(damage,other.gameObject.tag);
                        if (this.gameObject.tag == "Bullet")
                        {
                            PhotonNetwork.Destroy(this.gameObject);
                        }
                        //PhotonNetwork.Destroy(this.gameObject);
                    }
                }
                return;
            }
            if (other.gameObject.tag == "test")
            {
                if(team==0)
                {
                    GameManager.instance.blues += 10;
                }
                if(team==1)
                {
                    GameManager.instance.reds += 10;
                }
            }
        }
        //if (other.GetComponent<PlayerInfo>().team != team) 
        //{
        //Debug.Log("work2");

        // }

        //IEnumerator wait()
        //{
        //    yield return new WaitForSeconds(2);
        //    PhotonNetwork.Destroy(this.gameObject);
        //}

        //if(other.gameObject.tag=="Player")
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(team);
        }
        if (stream.IsReading)
        {
            team = (int)stream.ReceiveNext();
        }
    }
}
