using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class Tower : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    //private string t;
    private int team;

    Transform target;

    private int o;
    //Dictionary<int, GameObject> order;
    SortedDictionary<int, GameObject> order;
    Queue<GameObject> order3;
    List<GameObject> order2;
    Queue<GameObject> order4;

    float lasyshot;

    public float coolDown;
    public float warn;
    public float timeRemaining = 10;

    private void Awake()
    {
        if (this.transform.parent.gameObject.tag == "RedCrystal") team = 1;
        if (this.transform.parent.gameObject.tag == "BlueCrystal") team = 0;
        //if (gameObject.tag == "RedCrystal") team = 1;
        //if (gameObject.tag == "BlueCrystal") team = 0;
    }
    void Start()
    {
        //o = 0;
        warn = timeRemaining;
        //order = new SortedDictionary<int, GameObject>();
        //order2 = new List<GameObject>();
        //f (photonView.IsMine)
        //{
        order3 = new Queue<GameObject>();
        order4 = new Queue<GameObject>();
        //}
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (photonView.IsMine)
        //{
            if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerInfo>().team != this.team)
            {
                //order.Add(o, other.gameObject);
                //o++;
                //order2.Add(other.gameObject);
                order3.Enqueue(other.gameObject);

            }
            if (other.gameObject.tag == "Monster" && other.gameObject.GetComponent<Monster>().team != this.team)
            {
                order4.Enqueue(other.gameObject);
            }
       // }
    }

    public void OnTriggerStay(Collider other)
    {
       //if (photonView.IsMine)
       //{
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerInfo>().team != this.team)
            {
                bool enter = false;
            //target = other.transform;
            //target = order[order.Keys.Min()].transform;
            //target = order2.
            //target = order4.Peek().transform;
            if (!order3.Contains(other.gameObject))
            {
                order3.Enqueue(other.gameObject);
            }
            if (order4.Count == 0)
            {
                target = order3.Peek().transform;
            }

            //if (order3.Peek().gameObject.GetComponent<PlayerInfo>().getkill)
            //{
                //order4.Peek().gameObject.GetComponent<Monster>().approve = true;
                //order3.Dequeue();
                //order3.Clear();
            //}

            //tar = other.gameObject;
            if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    //Debug.Log(timeRemaining);
                }
                else { enter = true; }

                //Debug.Log(other.gameObject.GetComponent<PlayerInfo>().team);
                if (enter)
                {

                    //if (other.gameObject.GetComponent<PlayerInfo>().team != team)
                    //{
                    //if (other.gameObject.CompareTag("Player"))
                    //{
                    //if(other.GetComponent<playerController>().team)
                    photonView.RPC("Shoot", RpcTarget.All);
                    //Shoot();
                    enter = false;
                    //}
                    //}
                }
            }
            if (other.gameObject.CompareTag("Monster") && other.gameObject.GetComponent<Monster>().team != this.team)
            {
                bool enter = false;
            //target = other.transform;
            //target = order[order.Keys.Min()].transform;
            //target = order2.
            //target = order4.Peek().transform;
                if (!order4.Contains(other.gameObject)) 
                {    
                    order4.Enqueue(other.gameObject); 
                }
                if (order4.Count != 0)
                {
                    if (order4.Peek().gameObject != null && !order4.Peek().gameObject.GetComponent<Monster>().dead)
                    {
                        target = order4.Peek().transform;
                    }
                    if (order4.Peek().gameObject.GetComponent<Monster>().dead)
                    {
                        //order4.Peek().gameObject.GetComponent<Monster>().approve = true;
                        order4.Clear();
                    }

                }
                else if (order4.Count==0&&order3.Count!=0)
                {
                    target = order3.Peek().transform;
;               }
                else 
                {
                    target = null;
                }

                


                //Debug.Log(target.gameObject);

                //tar = other.gameObject;
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    Debug.Log(timeRemaining);
                }
                else { enter = true; }

                //Debug.Log(other.gameObject.GetComponent<PlayerInfo>().team);
                if (enter)
                {

                    //if (other.gameObject.GetComponent<PlayerInfo>().team != team)
                    //{
                    //if (other.gameObject.CompareTag("Player"))
                    //{
                    //if(other.GetComponent<playerController>().team)
                    photonView.RPC("Shoot", RpcTarget.All);
                    //Shoot();
                    enter = false;
                    //}
                    //}
                }
            }
            else return;
       // }
        //if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerInfo>().team != this.team)
        //{
        //    bool enter = false;
        //    target = other.transform;
        //    //tar = other.gameObject;
        //    if (timeRemaining > 0)
        //    {
        //        timeRemaining -= Time.deltaTime;
        //        Debug.Log(timeRemaining);
        //    }
        //    else { enter = true; }

        //    //Debug.Log(other.gameObject.GetComponent<PlayerInfo>().team);
        //    if (enter)
        //    {

        //        //if (other.gameObject.GetComponent<PlayerInfo>().team != team)
        //        //{
        //        //if (other.gameObject.CompareTag("Player"))
        //        //{
        //        //if(other.GetComponent<playerController>().team)
        //        photonView.RPC("Shoot", RpcTarget.All);
        //        //Shoot();
        //        enter = false;
        //        //}
        //        //}
        //    }
        //}
        //else return;


    }

    private void OnTriggerExit(Collider other)
    {
       //if (photonView.IsMine)
       //{
            if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerInfo>().team != this.team)
            {
            //timeRemaining = warn;
            //order.Remove(GetKeyFromValue(other.gameObject));
                if (order3.Peek().gameObject.GetComponent<PhotonView>().ViewID == other.gameObject.GetComponent<PhotonView>().ViewID)
                {
                    order3.Clear();
                //order3.Dequeue();
                }
                else return;
                //order2.Remove(other.gameObject);
                photonView.RPC("Warning", RpcTarget.All);
            }
            if (other.gameObject.tag == "Monster" && other.gameObject.GetComponent<Monster>().team != this.team)
            {
                //order4.Dequeue();
                if (order4.Peek().gameObject.GetComponent<PhotonView>().ViewID == other.gameObject.GetComponent<PhotonView>().ViewID)
                {
                    order4.Clear();
                //order4.Dequeue();
                }
                else return;
            //order2.Remove(other.gameObject);
                photonView.RPC("Warning", RpcTarget.All);
            }
       // }
    }

    public int GetKeyFromValue(GameObject valueVar)
    {
        foreach (int keyVar in order.Keys)
        {
            if (order[keyVar] == valueVar)
            {
                return keyVar;
            }
        }
        return 0;
    }


    [PunRPC]
    void Shoot()
    {
       //if (PhotonNetwork.IsMasterClient)
       //{
            //if (target.gameObject.GetComponent<PlayerInfo>().team != this.team)
            //{
            //GameObject nt = new GameObject();
            //nt.transform.SetPositionAndRotation(target, taro);

            if ((1 * Time.time) - lasyshot < coolDown)
            {
                return;
            }
            else
            {
                lasyshot = 1 * Time.time;
                //GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                //GameObject bulletGO = MasterManager.NetworkInstantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                GameObject bulletGO = PhotonNetwork.InstantiateRoomObject(bulletPrefab.name, firePoint.position, firePoint.rotation);
                //Bullet bullet = bulletGO.GetComponent<Bullet>();
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("bigbullet"))
                {
                    item.GetComponent<Bullet>().Seek(target);
                }
                //if (bulletGO.GetComponent<Bullet>() != null)
                //{
                //  bulletGO.GetComponent<Bullet>().Seek(target);
                //}
                //if (bullet != null)
                //{
                    //bullet.Seek(target,taro);
                  //  bullet.Seek(target);
                //}
            }
        //}
    }


    [PunRPC]
    void Warning()
    {
        timeRemaining = warn;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        { 
            stream.SendNext(team);
            //stream.SendNext(target.position);
            //stream.SendNext(target.rotation);
        }
        if (stream.IsReading)
        {
            team = (int)stream.ReceiveNext();
            //target.position = (Vector3)stream.ReceiveNext();
            //target.rotation = (Quaternion)stream.ReceiveNext();
        }
    }



    void whattoshoot(Vector3 center, float radius)
    {
        //Collider[] hitColliers = Physics.OverlapSphere(center, radius);
         
        //foreach (var item in hitColliers)
        //{
        //    if (item.gameObject.tag == "Player"&&hitColliers[0]==item)
        //    {

        //    }
        //}
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        TakeDamage(50);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Bullet")
    //    {
    //        TakeDamage(50);
    //    }
    //}

    //public void TakeDamage(float damage)
    //{
    //    photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    //}


    //[PunRPC]
    //private void RPC_TakeDamage(float damage) 
    //{
    //    health -= damage;
    //    healthbar.value = health;
    //}
}
