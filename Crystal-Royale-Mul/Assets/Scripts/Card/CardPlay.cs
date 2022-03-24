using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class CardPlay : MonoBehaviourPun
{
    private List<GameObject> deck;
    private List<GameObject> chand;
    private GameObject[] onhand;
    public bool IsAvailable = true;


    public GameObject cardleft;


    private int i;
    private int team;
    [SerializeField] private GameObject[] cardslot;
    private GameObject[] c;
    //private int index;

    [SerializeField]
    private GameObject random;

    

    [SerializeField]
    private GameObject cd;

    private float cdd;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected) PhotonNetwork.OfflineMode = true;
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        //cardslot = new GameObject[3];
        deck = new List<GameObject>();
        onhand = new GameObject[3];
        chand = new List<GameObject>();

        //c = new GameObject[3];
        foreach (GameObject g in Resources.LoadAll("Card", typeof(GameObject)))
        {
            Debug.Log("prefab found: " + g.name);
            deck.Add(g);
        }
    }

    private void Start()
    {
        i = 0;
        cdd = 0;
        for (int i = 0; i < onhand.Length; i++)
        {
            if (deck.Count == 0) break;
            onhand[i] = deck[Random.Range(0, deck.Count-1)];
            Debug.Log(onhand[i]);
            //c[i]=(Instantiate(onhand[i], cardslot[i].transform.position, cardslot[i].transform.rotation));
            chand.Insert(i, Instantiate(onhand[i], cardslot[i].transform.position, cardslot[i].transform.rotation));
            //deck.RemoveAt(deck.IndexOf(onhand[i]));
            deck.Remove(onhand[i]);
            chand[i].transform.SetParent(cardslot[i].transform);
            //c[i].transform.SetParent(cardslot[i].transform);   
        }
    }

    private void ran(int index)
    {  
        if (deck.Count == 0) return;
        //Destroy(c[index].gameObject);
        onhand[index] = deck[Random.Range(0, deck.Count-1)];
        deck.Remove(onhand[index]);
        chand.Insert(index, Instantiate(onhand[index], cardslot[index].transform.position, cardslot[index].transform.rotation));
        chand[index].transform.SetParent(cardslot[index].transform);
        //c[index] = Instantiate(onhand[index], cardslot[index].transform.position, cardslot[index].transform.rotation);
        //c[index].transform.SetParent(cardslot[index].transform);
    }

    public void manran()
    {
        int cost = 80;
        if(team == 0 && cost <= GameManager.instance.blues) 
        {
            if (chand.Count == 3 && deck.Count >= 3)
            {
                GameManager.instance.blues -= cost;
                for (int i = 0; i < cardslot.Length; i++)
                {
                    Destroy(cardslot[i].transform.GetChild(0).gameObject);
                }
                for (int i = 0; i < onhand.Length; i++)
                {
                    GameObject pick;
                    pick = deck[Random.Range(0, deck.Count-1)];
                    deck.Add(onhand[i]);
                    onhand[i] = pick;
                    chand.Remove(chand[i]);
                    //Destroy(chand[i]);
                    deck.Remove(onhand[i]);
                    chand.Insert(i, Instantiate(onhand[i], cardslot[i].transform.position, cardslot[i].transform.rotation));
                    chand[i].transform.SetParent(cardslot[i].transform);
                }
            }
            else return;
        }
        if (team == 1 && cost <= GameManager.instance.reds)
        {
            GameManager.instance.reds -= cost;
            if (chand.Count == 3 && deck.Count >= 3)
            {
                for (int i = 0; i < cardslot.Length; i++)
                {
                    Destroy(cardslot[i].transform.GetChild(0).gameObject);
                }
                chand = new List<GameObject>();
                for (int i = 0; i < onhand.Length; i++)
                {
                    GameObject pick;
                    pick = deck[Random.Range(0, deck.Count - 1)];
                    deck.Add(onhand[i]);
                    onhand[i] = pick;
                    //Destroy(chand[i]);
                    deck.Remove(pick);
                    chand.Insert(i, Instantiate(onhand[i], cardslot[i].transform.position, cardslot[i].transform.rotation));
                    chand[i].transform.SetParent(cardslot[i].transform);
                }
                Debug.Log(deck.Count);
            }
            else return;
        }
    }

    private void LateUpdate()
    {

        //foreach (GameObject card in chand)
        //{
        for (int i = 0; i < chand.Count; i++)
        {
            if (chand[i] != null)
            {
                //Debug.Log(chand[i].GetComponent<DragDrop>().set);
                if (chand[i].GetComponent<DragDrop>().set)
                {
                    random.SetActive(false);
                    if (team == 0)
                    {
                        GameManager.instance.blues -= chand[i].GetComponent<DragDrop>().card.cost;
                    }
                    if (team == 1)
                    {
                        GameManager.instance.reds -= chand[i].GetComponent<DragDrop>().card.cost;
                    }
                    Debug.Log(chand[i].GetComponent<DragDrop>().set);
                    int pos = chand.IndexOf(chand[i]);
                    cdd = chand[i].GetComponent<DragDrop>().card.cooldown;
                    IsAvailable = false;
                    cd.SetActive(true);
                    for (int j = 0; j < cardslot.Length; j++)
                    {
                        cardslot[j].gameObject.SetActive(false);
                    }
                    //StartCoroutine(Cooldown(chand[i].GetComponent<DragDrop>().card.cooldown));
                    Destroy(chand[i]);
                    chand.Remove(chand[i]);
                    ran(pos);
                }
            }
        }

        if(cdd>=0)
        {
            cdd -= Time.deltaTime;
            cd.GetComponent<Text>().text = cdd.ToString("0");
        }
        else
        {
            cd.SetActive(false);
            for (int i = 0; i < cardslot.Length; i++)
            {
                cardslot[i].gameObject.SetActive(true);
            }
            IsAvailable = true;
            random.SetActive(true);
        }
        if (deck.Count < 3)
        {
            random.SetActive(false);
        }

        cardleft.GetComponent<Text>().text = (deck.Count + chand.Count).ToString("0");
        //if (card != null)
        //{
        //    Debug.Log(card.GetComponent<DragDrop>().set);
        //    if (card.GetComponent<DragDrop>().set)
        //    {
        //        int pos = chand.IndexOf(card);
        //        //Destroy(chand[i].gameObject);
        //        chand.Remove(card);
        //        ran(pos);
        //    }
        //}
        //}
        //if (chand[i] != null)
        //{
        //    Debug.Log(chand[i].GetComponent<DragDrop>().set);
        //    if (chand[i].GetComponent<DragDrop>().set)
        //    {
        //        //Destroy(chand[i].gameObject);
        //        chand.RemoveAt(i);
        //        ran(i);
        //    }
        //}
        //else break;

        //while (c.Length != 0) 
        //{
        //    if (c[i] != null)
        //    {
        //        Debug.Log(c[i].GetComponent<DragDrop>().set);
        //        if (c[i].GetComponent<DragDrop>().set)
        //        {
        //            Destroy(c[i].gameObject);
        //            ran(i);
        //        }
        //    }
        //    else break;
        //}

        //for (int index = 0; index < c.Length; index++)
        //{

        //    if (c[index] != null)
        //    {
        //        Debug.Log(c[index].GetComponent<DragDrop>().set);
        //        if (c[index].GetComponent<DragDrop>().set)
        //        {
        //            deck.RemoveAt(deck.IndexOf(c[index]));
        //            Destroy(c[index]);
        //            if (deck.Count == 0)
        //            {
        //                Destroy(c[index].gameObject);
        //                ran(index);
        //                continue;
        //            }
        //            Destroy(c[index].gameObject);
        //            ran(index);
        //            deck.Remove(c[index]);
        //            ran(index);
        //        }
        //        if(c[index].gameObject.GetComponent<DragDrop>().card.used == false)
        //        {
        //            break;
        //        }
        //    }
        //    else break;
        //}
    }

    //IEnumerator Cooldown(float cooldown)
    //{
        //IsAvailable = false;
        //for (int i = 0; i < cardslot.Length; i++)
        //{
        //    cardslot[i].gameObject.SetActive(false);
        //}
        //yield return new WaitForSeconds(cooldown);
        //for (int i = 0; i < cardslot.Length; i++)
        //{
        //    cardslot[i].gameObject.SetActive(true);
        //}
        //IsAvailable = true;
        //random.SetActive(true);
   //}



}
