using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotdis : MonoBehaviour
{
 float shotSpeed =20.0f;
 Transform hero;
 int MaxDist = 10;
 
 void Start()
    {
        /*THIS FIXED IT. I DRAGGED MY PLAYER PREFAB FROM THE PROJECT FOLDER INTO HERO IN THE INSPECTOR BECAUSE I COULDN'T DRAG THE PLAYER PREFAB CLONE THAT I HAD DRAGGED INTO THE SCENE OR HIERARCHY INTO HERO IN THE INSPECTOR. THE PLAYER PREFAB IN THE PROJECT FOLDER DOESN'T MOVE FOR ANYBODY WONDERING WHY IT WOULD MATTER.*/
        hero = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.Translate(0, 0, shotSpeed * Time.deltaTime);
        ShotDistance();
    }

    void ShotDistance()
    {
        if (Vector3.Distance/*NEEDS TO BE A CAPITAL D IN DISTANCE!!!*/(hero.position, transform.position) > MaxDist)
        {
            Destroy(gameObject);
        }
    }
}
