using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Attack_Fire : MonoBehaviourPun
{

    public GameObject bulletPrefabs;
    public GameObject ultPrefabs;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    float lastshot;
    float lastult;
    public float coolDown;
    public float coolDownUlt;
    public Animator anim;

    
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip fb;

    [SerializeField]
    private AudioClip mt;

    private Camera camera;

    [SerializeField]
    private GameObject s;
    [SerializeField]
    private GameObject c;

    private int order;
    // Start is called before the first frame update
    void Start()
    {
        order = 2;
        if (photonView.IsMine)
        {
            audioSource = this.GetComponent<AudioSource>();
            if (order == 2)
            {
                s = GameObject.FindGameObjectWithTag("skill2");
                s.GetComponent<Text>().text = "Meteor Flame";
                c = GameObject.FindGameObjectWithTag("cd2");
                c.GetComponent<Text>().text = "Ready";
            }
            cursor = GameObject.FindGameObjectWithTag("cursor"); //cursor must be on the game map
            camera = Camera.main;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            LaunchProjectile();
            //c.GetComponent<Text>().text = "Ready";
            if (c != null)
            {
                if (Time.time - lastult < coolDownUlt)
                {
                    c.GetComponent<Text>().text = (coolDownUlt - Time.time + lastult).ToString("0");
                }
                else c.GetComponent<Text>().text = "Ready";
            }
        }

    }
    public void LaunchProjectile()
    {
        
        if (anim == null)
        {
            anim = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
        }
        if (camera != null)
        {
            Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, 100f, layer))
            {
                cursor.SetActive(true);
                cursor.transform.position = hit.point + Vector3.up * 0.1f;
                Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);
                if (Time.time - lastshot < coolDown)
                {
                    anim.SetFloat("Attack", 0);
                    Debug.Log("On cool down");
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lastshot = Time.time;
                        audioSource.PlayOneShot(fb);
                        returnLookat();
                        anim.SetFloat("Attack", 1);
                        GameObject obj = MasterManager.NetworkInstantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
                        obj.GetComponent<Rigidbody>().velocity = Vo;
                    }
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        if (Time.time - lastult < coolDownUlt)
                        {
                            anim.SetFloat("Attack", 0);
                            Debug.Log("On cool down");
                            
                        }
                        else
                        {
                            audioSource.PlayOneShot(mt);
                            anim.SetFloat("Attack", 1);
                            Vector3 V = CalculateVelocity(hit.point, shootPoint.position, 3f);
                            lastult = Time.time;
                            returnLookat();
                            GameObject obj = MasterManager.NetworkInstantiate(ultPrefabs, shootPoint.position, Quaternion.identity);
                            obj.GetComponent<Rigidbody>().velocity = V;
                        }
                    }
                }
            }
            else
            {
                anim.SetFloat("Attack", 0);
                cursor.SetActive(false);
            }
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;
        //create a float that represent our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
    public Vector3 returnLookat()
    {
        Vector3 Lookat = Vector3.zero;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, gameObject.transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            gameObject.transform.LookAt(hitPoint);
            Lookat = hitPoint;
        }
        return Lookat;
    }
}



