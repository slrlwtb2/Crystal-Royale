using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Card",fileName = "New Card")]
public class Card : ScriptableObject
{
    public string cardname;
    public string effect;
    public string type;
    public int team;

    public float health;
    public int cost;
    public int amount;

    public float duration;
    public float cooldown;

    public float damage;
    public float heal;

    
    public GameObject minion;
    public GameObject area;
    public GameObject item;
}
