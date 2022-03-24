using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_attack : ScriptableObject
{
    public float cooldownTime;
    public float activeTime;

    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;

    public virtual void Activate(GameObject obj)
    {

    }
    public virtual void BeginCooldown(GameObject obj)
    {

    }
}
