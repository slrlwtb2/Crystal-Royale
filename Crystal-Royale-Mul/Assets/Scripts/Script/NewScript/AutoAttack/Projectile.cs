using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }
    void LaunchProjectile() 
    {
        Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, 100f, layer)) 
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);

           // transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0)) 
            {
                GameObject obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
                obj.GetComponent<Rigidbody>().velocity = Vo;
            }
        }
        else 
        {
            cursor.SetActive(false);
        }
    }
    Vector3 CalculateVelocity(Vector3 target,Vector3 origin,float time) 
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
}
