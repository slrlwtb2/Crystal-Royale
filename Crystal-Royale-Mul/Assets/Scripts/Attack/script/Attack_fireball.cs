using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Auto_Atatck/Fire")]
public class Attack_fireball : MonoBehaviour
{
    public GameObject indicator;
    public Attack_Fire launchFire;
    float lastshot;
    public float coolDown;
    void Start()
    {


    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            indicator.SetActive(true);
            launchFire.LaunchProjectile();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - lastshot < coolDown)
            {
                Debug.Log("On cool down");
            }
            else
            {
                lastshot = Time.time;
                

            }
        }
    }
}
