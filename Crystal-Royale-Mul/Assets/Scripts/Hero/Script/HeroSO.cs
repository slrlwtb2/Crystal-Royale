using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Hero/HeroSO")]
public class HeroSO : ScriptableObject
{

    public string name;
    public GameObject model;
    public float hp;
    public float speed;
    public float atk;
    public float AttackRange;
    
}
