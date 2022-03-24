using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public GameObject bulletPrefabs;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    float lastshot;
    public float coolDown;
    public Animator anim;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {

        cursor = GameObject.FindGameObjectWithTag("cursor"); //cursor must be on the game map
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }
    public void LaunchProjectile()
    {
        if (anim == null)
        {
            anim = gameObject.GetComponent<PlayerController3>().anim; //Because this script cannot fetch animator from playerctr3 at the same time 
        }
        Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 3f);
            if (Time.time - lastshot < coolDown)
            {
                Debug.Log("On cool down");
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    lastshot = Time.time;
                    returnLookat();
                    anim.SetFloat("Attack", 1);
                    GameObject obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
                    obj.GetComponent<Rigidbody>().velocity = Vo;
                    
                }
            }
        }
        else { anim.SetFloat("Attack", 0); cursor.SetActive(false); }
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



