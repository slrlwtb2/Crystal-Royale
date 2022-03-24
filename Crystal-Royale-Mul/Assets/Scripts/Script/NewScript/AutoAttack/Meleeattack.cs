using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meleeattack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]

    GameObject player;

    float lasyshot;
    public float coolDown;


    bool isFired;

    Animator anim;


    void Start()
    {
        isFired = false;
        player = gameObject;
        anim = gameObject.GetComponentInChildren<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0))
        {


            if (Time.time - lasyshot < coolDown)
            {
                Debug.Log("On cool down");
            }
            else
            {
                lasyshot = Time.time;
                anim.SetFloat("Attack", 1);
            }

        }


        else { anim.SetFloat("Attack", 0); }
    }
}

