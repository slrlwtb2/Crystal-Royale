using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public enum MinionStates {Idle,Run,TakeDamage,Attack,Die}

public abstract class Minion : MonoBehaviourPunCallbacks, IDamageable
{
    //[SerializeField]
    public Card card;

    //[HideInInspector]
    //public GameObject skin;

    [HideInInspector]
    public MinionStates currentState;

    [HideInInspector]
    public bool cooldown = false;
    [HideInInspector]
    public float counter = 5.0f;


    public float health;
    [HideInInspector]
    public float attack;

    [HideInInspector]
    public NavMeshAgent agent;

    //[HideInInspector]
    //public int team;
    //[HideInInspector]
    //public GameObject target;

    [HideInInspector]
    public List<GameObject> enemyplayer;
    [HideInInspector]
    public List<GameObject> playerlist;

    private void Awake()
    {
  
        //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        //skin = GetComponentInChildren<SkinnedMeshRenderer>();
        
        //Henshin(card.cardname);
        //playerlist = new List<GameObject>();
        //playerlist.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //enemyplayer = new List<GameObject>();
        //agent = GetComponent<NavMeshAgent>();
        Debug.Log(playerlist.Count);
        foreach (GameObject item in playerlist)
        {
            Debug.Log(item);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //health = card.health;
        //attack = card.damage;
    }

    public abstract void RuntoEnemy();

    public abstract float GetAttack();

    public abstract void Attack();

    public abstract void FindEnemy();

    public abstract void TakeDamage(float damage);

    public abstract void StateMachine();

    public abstract void Die();


    public abstract void Henshin(string name, int viewid, int t);

}
