using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Plane plane;
    Vector3 dis;
    public float disz;

    public Card card;
    Canvas canvas;
    private GameObject cancel;
    private GameObject realcancel;

    private RectTransform rectTransform;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 start;
    private Vector3 startscale;
    private GraphicRaycaster graphicRaycaster;
    

    public bool set;
    private int team;
    [SerializeField]
    private GameObject cardobj;
    private int monamount;
    
    private void Awake()
    {        
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        rectTransform = GetComponent<RectTransform>();
        start = rectTransform.position;
        startscale = rectTransform.localScale;
        //minion = card.minion;
       
        Canvas[] Canvasgui = FindObjectsOfType<Canvas>();
        foreach (Canvas item in Canvasgui)
        {
            if (item.name == "Canvas")
            {
                canvas = item;
            }
        }
        
    }

    private void Ctype(Card card)
    {
        if (card.type == "Area") { cardobj = card.area; }
        if (card.type == "Minion")
        {
            cardobj = card.minion;
            monamount = card.amount;
        }
    }


    private void Start()
    { 
        cancel = GameObject.FindGameObjectWithTag("Cancel");
        realcancel =  cancel.gameObject.transform.GetChild(0).gameObject;
        realcancel.SetActive(false);
        Ctype(card);
        //cancel.gameObject.SetActive(false);
        //cardobj = card.minion;
        dis = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - disz);
        plane = new Plane(Vector3.forward, dis);
        set = false;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        
    }
    public void OnDrag(PointerEventData eventData)
    { 
        Debug.Log("Drag");
        rectTransform.localScale = new Vector3(0.5f,0.5f,0.5f);
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
        realcancel.SetActive(true);
        //rectTransform.anchoredPosition += eventData.delta/canvas.GetComponentInChildren<Canvas>().scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        canvas.GetComponent<GraphicRaycasterRaycasterExample>().detect();
        if (canvas.GetComponent<GraphicRaycasterRaycasterExample>().cancel)
        {
            //if (hit.collider.transform.tag == "Cancel")
            //{
            rectTransform.position = start;
            rectTransform.localScale = startscale;
            realcancel.SetActive(false);
            canvas.GetComponent<GraphicRaycasterRaycasterExample>().cancel = false;
            //}
        }
        else
        {
            if (this.card.cost > GameManager.instance.GetScore(team))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    rectTransform.position = start;
                    rectTransform.localScale = startscale;
                    realcancel.SetActive(false);
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        Debug.Log(hit.point);

                        if (hit.collider.gameObject.tag == "prohibit"|| hit.collider.gameObject.tag == "test")
                        {
                            rectTransform.position = start;
                            rectTransform.localScale = startscale;
                            realcancel.SetActive(false);
                            return;
                        }
                        else
                        {
                            //else
                            // {
                            //if (EventSystem.current.IsPointerOverGameObject(aimid))
                            //{
                            //    rectTransform.position = start;
                            //    rectTransform.localScale = startscale;
                            //    realcancel.SetActive(false);
                            //}
                            //else
                            //{
                            //float npos = hit.point.y;
                            //Vector3 n = new Vector3(hit.point.x, npos + minion.transform.localScale.y/2, hit.point.z);
                            //cancel.gameObject.SetActive(false);

                            //Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z);
                            if (team == 0)
                            {
                                if (monamount > 0 && card.type == "Minion")
                                {
                                    for (int i = 0; i < monamount; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z);
                                            MasterManager.NetworkInstantiate(cardobj, n, Quaternion.Euler(0, 180, 0));
                                        }
                                        else
                                        {
                                            Vector3 n = new Vector3(hit.point.x,hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z + Random.Range(0,monamount));
                                            MasterManager.NetworkInstantiate(cardobj, n, Quaternion.Euler(0, 180, 0));
                                        }
                                    }
                                }
                                else if(monamount == 0)
                                {
                                    Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z);
                                    MasterManager.NetworkInstantiate(cardobj, n, Quaternion.Euler(0, 180, 0));
                                }

                            }
                            else
                            {
                                if (monamount > 0 && card.type == "Minion")
                                {
                                    for (int i = 0; i < monamount; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z);
                                            MasterManager.NetworkInstantiate(cardobj, n, Quaternion.identity);
                                        }
                                        else
                                        {
                                            Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z + Random.Range(0, monamount));
                                            MasterManager.NetworkInstantiate(cardobj, n, Quaternion.identity);
                                        }
                                    }
                                }
                                else if(monamount ==0)
                                {
                                    Vector3 n = new Vector3(hit.point.x, hit.point.y + cardobj.transform.localScale.y / 2, hit.point.z);
                                    MasterManager.NetworkInstantiate(cardobj, n, Quaternion.identity);
                                }
                            }
                            // MasterManager.NetworkInstantiate(cardobj, n , Quaternion.identity);
                            set = true;
                            realcancel.SetActive(false);
                            //MasterManager.Instantiate(minion, n, Quaternion.identity);
                            //Destroy(this.gameObject);
                            //Vector3 n = new Vector3(hit.point.x, npos + GameObject.Find("ms04_Mushroom_1").transform.localScale.y / 2, hit.point.z);
                            //Instantiate(GameObject.Find("ms04_Mushroom_1"), n, Quaternion.identity);
                            //}
                            //}
                        }
                    }
                    //else set = false;

                }

            }
        }

        //if (set == false)
        //{
        //    rectTransform.position = start;
        //}
        //else if(set == true) card.used = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointerdown");
    }
}
