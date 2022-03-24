using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Monster : Minion, IPunObservable
{
    private Animator anim;
    public float dis;
    public GameObject targetenemy;
    public GameObject target;
    public int team;

    [SerializeField]
    private GameObject realgo;
    //private SkinnedMeshRenderer sk;
    private GameObject skin;
    [SerializeField]
    private Slider healthbar;

    public bool dead;
    public bool approve;


    void Awake()
    {
        targetenemy = GameObject.FindGameObjectWithTag("Target");
        target = targetenemy.GetComponent<TargetingEnemy>().target;
    }
    void Start()
    {
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        health = card.health;
        healthbar.maxValue = health;
        healthbar.minValue = 0;
        approve = false;
        dead = false;
        healthbar.value = health;
        if (card.cardname == "Golem")
        {
            skin = this.gameObject.transform.Find("ms01_Golem_1").gameObject;
            realgo = skin.gameObject.transform.Find("ms01_Golem").gameObject;
        }
        if(card.cardname == "Bee")
        {
            skin = this.gameObject.transform.Find("ms03_Bee_2").gameObject;
            realgo = skin.gameObject.transform.Find("ms03_Bee").gameObject;
        }
        if(card.cardname == "Leshy")
        {
            skin = this.gameObject.transform.Find("ms05_Leshy_2").gameObject;
            realgo = skin.gameObject.transform.Find("ms05_Leshy").gameObject;
        }
        if(card.cardname == "Mushroom")
        {
            realgo = this.gameObject.transform.Find("ms04_mushroom").gameObject;
        }
        //sk = realgo.GetComponent<SkinnedMeshRenderer>();
        Debug.Log(card.cardname);
        if (photonView.IsMine)
        {
            photonView.RPC("Henshin", RpcTarget.All, card.cardname, this.gameObject.GetComponent<PhotonView>().ViewID, team);
            //Henshin(card.cardname);
        }
        attack = card.damage;
        
        agent = GetComponent<NavMeshAgent>();
        //currentState = MinionStates.Idle;
        //anim = GetComponentInChildren<Animator>();
        //FindEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //Random.InitState((int)Time.time);
        if (targetenemy.GetComponent<TargetingEnemy>().target == null)
        {
            target = null;
        }
        else target = targetenemy.GetComponent<TargetingEnemy>().target;
        //Debug.Log(target);
        //target = TargetingEnemy.target;
        //FindEnemy();
        //anim.SetBool("Idle", true);

        //if (target == null)
        //{
        //    foreach (GameObject player in enemyplayer)
        //    {
        //        target = player;
        //    }
        //    agent.SetDestination(target.transform.position);
        //target = enemyplayer[Random.Range(0, enemyplayer.Count-1)];
        //}
        //else { agent.SetDestination(target.transform.position); }

        //if (target != null)
        //{
        //    dis = Vector3.Distance(transform.position, target.transform.position);
        //}
        //StateMachine();
        if (this.health <= 0)
        {
            //currentState = MinionStates.Die;;;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //currentState = MinionStates.TakeDamage;
            //TakeDamage(20);
        }
    }

    [PunRPC]
    public override void Henshin(string name, int viewid, int t)
    {
        //if (photonView.IsMine)
        //{
        // if (this.card.cardname == name)
        // {
        foreach (GameObject m in GameObject.FindGameObjectsWithTag("Monster"))
        {
            if (m.GetComponent<PhotonView>().ViewID == viewid)
            {
                if(t == 0)
                {
                    foreach (Material g in Resources.LoadAll("Materials/" + name, typeof(Material)))
                    {
                        if (t == 0 && g.name == "Blue")
                        {
                            if(m.GetComponent<Monster>().card.cardname == "Golem")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms01_Golem_1").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms01_Golem").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Leshy")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms05_Leshy_2").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms05_Leshy").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Bee")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms03_Bee_2").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms03_Bee").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Mushroom")
                            {
                                //m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms01_Golem_1").gameObject;
                                m.GetComponent<Monster>().realgo = m.gameObject.transform.Find("ms04_mushroom").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            //this.sk.material = g;
                        }
                    }
                }
                if (t == 1)
                {
                    foreach (Material g in Resources.LoadAll("Materials/" + name, typeof(Material)))
                    {

                        if (t == 1 && g.name == "Red")
                        {
                            if (m.GetComponent<Monster>().card.cardname == "Golem")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms01_Golem_1").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms01_Golem").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Leshy")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms05_Leshy_2").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms05_Leshy").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Bee")
                            {
                                m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms03_Bee_2").gameObject;
                                m.GetComponent<Monster>().realgo = m.GetComponent<Monster>().skin.gameObject.transform.Find("ms03_Bee").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            if (m.GetComponent<Monster>().card.cardname == "Mushroom")
                            {
                                //m.GetComponent<Monster>().skin = m.gameObject.transform.Find("ms01_Golem_1").gameObject;
                                m.GetComponent<Monster>().realgo = m.gameObject.transform.Find("ms04_mushroom").gameObject;
                                m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            }
                            //m.GetComponent<Monster>().realgo.GetComponent<SkinnedMeshRenderer>().material = g;
                            //this.sk.material = g;
                        }
                    }
                }
            }
        }
                //foreach (Material g in Resources.LoadAll("Materials/" + name, typeof(Material)))
                //{
                //    if (team == 0 && g.name == "Blue")
                //    {
                //        this.sk.material = g;
                //    }
                //    if (team == 1 && g.name == "Red")
                //    {
                //        this.sk.material = g;
                //    }
                //}
          //  }
        //
    }
    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Bullet")
    //    {
    //        currentState = MinionStates.Idle;
    //    }
    //}

    public override void FindEnemy()
    {
        //anim.SetFloat("Run", 0);
        //anim.SetFloat("TakeDamage", 1);
        //foreach (GameObject player in playerlist)
        //{
        //    if (player.GetComponent<PlayerInfo>().team != this.team)
        //    {
        //        Debug.Log(player.GetComponent<PlayerInfo>().team);
        //        enemyplayer.Add(player);
        //    }
        //}
        if (enemyplayer.Count == 0)
        {
            //currentState = MinionStates.Idle;
            foreach (GameObject player in playerlist)
            {
                if (player.GetComponent<PlayerInfo>().team != this.team)
                {
                    Debug.Log(player.GetComponent<PlayerInfo>().team);
                    enemyplayer.Add(player);
                }
            }
            if (enemyplayer.Count == 0) return;
            //foreach (GameObject player in playerlist)
            //{
            //    if (player.GetComponent<PlayerInfo>().team != this.team)
            //    {
            //        Debug.Log(player.GetComponent<PlayerInfo>().team);
            //        enemyplayer.Add(player);
            //    }
            //else currentState = MinionStates.Idle;
            //if (target == null&&enemyplayer.Count==1)
            //{
            //    target = enemyplayer[Random.Range(0, enemyplayer.Count)];
            //    currentState = MinionStates.Run;
            //}
            //if (target == null)
            //{
            //    foreach (GameObject p in enemyplayer)
            //    {
            //        target = p;
            //    }
            //    currentState = MinionStates.Run;
            //target = enemyplayer[Random.Range(0, enemyplayer.Count)];
            //}
            //else { agent.SetDestination(target.transform.position); }
            //}
            //if (enemyplayer.Count == 0)
            //{
            //    currentState = MinionStates.Idle;
            //    return;
            //}

        }
        if (target == null) target = enemyplayer[0];
        if (target != null) return;
        //if (enemyplayer.Count != 0 && target != null)
        //{
        //    //target = enemyplayer[0];
        //    //currentState = MinionStates.Run;
        //    dis = Vector3.Distance(transform.position, target.transform.position);
        //}
        //else if (enemyplayer.Count == 0) 
        //{
        //    anim.SetBool("Idle", true);
        //    currentState = MinionStates.Idle;
        //    //return;
        //}
    }

    public override void RuntoEnemy()
    {
        //anim.SetFloat("Attack", 0);
        //if (target != null)
        //{
        if(target == null) currentState = MinionStates.Idle;
        //dis = Vector3.Distance(transform.position, target.transform.position);
            agent.SetDestination(target.transform.position);
            anim.SetBool("Run", true);
            //anim.SetFloat("Run", 1);
            if (dis <= 3f)
            {
                anim.SetBool("Run", false);
                agent.isStopped = true;
                currentState = MinionStates.Attack;
            }
            //if (dis >= 10f)
            //{
               // agent.isStopped = false;
                //agent.ResetPath();
                //agent.autoRepath = true;
                //anim.SetFloat("Run", 0);
                //currentState = MinionStates.Run;
            //}
            //agent.SetDestination(target.transform.position);
        //}
        
        //else currentState = MinionStates.Idle;
        //anim.SetBool("Idle", false);
        //anim.SetBool("Run", true);
        //anim.SetBool("Attack", false);
        //anim.SetBool("Run", true);
        //anim.SetFloat("Attack", 0);
        //anim.SetFloat("Run", 1);
        //agent.autoRepath = true;
      
    }

    public override void Die()
    {
       //anim.SetBool("Die", true);
        
    }

    
    public override void Attack()
    {
        if (photonView.IsMine)
        {
            if (target.gameObject != null)
            {
                if (target.gameObject.tag == "RedCrystal")
                {
                    if (!cooldown)
                    {
                        target.GetComponent<Crystal>().damage(this.card.damage, target.gameObject.tag);
                        //target.GetComponent<Crystal>().TakeDamage(50);
                        cooldown = true;
                    }
                    else
                    {
                        counter -= Time.deltaTime;
                        if (counter <= 0)
                        {
                            cooldown = false;
                            counter = 1f;
                        }
                    }
                }
                if (target.gameObject.tag == "BlueCrystal")
                {
                    if (!cooldown)
                    {
                        target.GetComponent<Crystal>().damage(this.card.damage, target.gameObject.tag);
                        cooldown = true;
                    }
                    else
                    {
                        counter -= Time.deltaTime;
                        if (counter <= 0)
                        {
                            cooldown = false;
                            counter = 1f;
                        }
                    }
                }
                if (target.gameObject.tag == "Player")
                {
                    if (!cooldown)
                    {
                        target.GetComponent<PlayerController3>().TakeDamage(this.card.damage);
                        cooldown = true;
                    }
                    else
                    {
                        counter -= Time.deltaTime;
                        if (counter <= 0)
                        {
                            cooldown = false;
                            counter = 1.5f;
                        }
                    }
                }
            }
            else
            {
                target = null;
            }
            //if (!cooldown)
            //{
            //    if (target.gameObject.tag == "RedCrystal")
            //    {
            //        target.GetComponent<Crystal>().TakeDamage(50);
            //        cooldown = true;
            //    }
            //    if (target.gameObject.tag == "BlueCrystal")
            //    {
            //        target.GetComponent<Crystal>().TakeDamage(50);
            //        cooldown = true;
            //    }
                //if(target.gameObject.tag=="Player")
                //{
                //    target.GetComponent<PlayerController3>().TakeDamage(this.attack);
                //    cooldown = true;
                //}
                //anim.SetFloat("Run", -1);
                //anim.SetBool("Attack", true);
            //}
            //else
            //{
                //anim.SetFloat("Attack", 0);
            //    counter -= Time.deltaTime;
            //    if (counter <= 0)
            //    {
            //        cooldown = false;
            //        counter = 5f;
            //    }
            //}
        }
      

        //if (dis > 5f)
        //{
        //    agent.isStopped = false;
        //    anim.SetBool("Attack", false);
        //    currentState = MinionStates.Run;
        //}
    }

    public override float GetAttack()
    {
        return this.health;
    }

    public override void TakeDamage(float damage)
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage)
    {
        if (health <= 0)
        {
            dead = true;
            healthbar.value = health;
            if(photonView.IsMine)
            {
                StartCoroutine(deadw());
                //PhotonNetwork.Destroy(gameObject);
            }
        }
        else
        {
            health -= damage;
            healthbar.value = health;
        }
    }

    public override void StateMachine()
    {
        //switch (currentState)
        //    {
        //        case MinionStates.Idle:
        //            if (target != null) currentState = MinionStates.Run;
        //            else FindEnemy();
        //            //anim.SetFloat("Run", 0);
        //            //anim.SetBool("TakeDamage", false);
        //            //anim.SetFloat("Attack", 0);
        //            //FindEnemy();
        //            break;
        //        case MinionStates.Run:
        //            RuntoEnemy();
        //            //anim.SetFloat("Attack", 0);
        //            //anim.SetFloat("Attack", -1);
        //            //anim.SetFloat("Run", 1);
        //            break;
        //        case MinionStates.TakeDamage:
        //            anim.SetBool("TakeDamage", true);
        //            TakeDamage(20);
        //            break;
        //        case MinionStates.Attack:
        //            Attack();
        //            //anim.SetFloat("Attack", 1);
        //            //anim.SetFloat("Run", 0);
        //            break;
        //        case MinionStates.Die:
        //            anim.SetBool("Die", true);
        //            Die();
        //            break;
        //        default:
        //            currentState = MinionStates.Idle;
        //            //anim.SetFloat("Idle", true);
        //            break;
        //    }
        
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(healthbar.value);
            stream.SendNext(attack);
            stream.SendNext(team);
            //stream.SendNext(skin);
        }
        if (stream.IsReading)
        {
            health = (float)stream.ReceiveNext();
            healthbar.value = (float)stream.ReceiveNext();
            attack = (float)stream.ReceiveNext();
            team = (int)stream.ReceiveNext();
        
            //skin = (SkinnedMeshRenderer)stream.ReceiveNext();
        }
    }



    IEnumerator deadw()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (photonView.IsMine)
    //    {
    //        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerInfo>().team != this.team)
    //        {
    //            currentState = MinionStates.Attack;
    //            Debug.Log("work?");
    //            Debug.Log(this.attack);
    //            //collision.gameObject.GetComponent<playerController>().TakeDamage(attack);
    //            Debug.Log("or not?");
    //            //collision.gameObject.GetComponent<playerController>().TakeDamage(this.attack);
    //            //collision.gameObject.GetComponent<PlayerInfo>().current_health -= attack;
    //        }
    //    }

    //}

    //public void OnCollisionStay(Collision collision)
    //{
    //    if (photonView.IsMine)
    //    {
    //        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerInfo>().team != team)
    //        {
    //            Debug.Log("work");
    //            collision.gameObject.GetComponent<playerController>().TakeDamage(attack);
    //            //collision.gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(-this.transform.position.x,this.transform.position.y,-this.transform.position.z));
    //        }
    //    }
    //}

    //public void OnCollisionExit(Collision collision)
    //{
    //    if (photonView.IsMine)
    //    {
    //        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerInfo>().team != team)
    //        {
    //            currentState = MinionStates.Run;
    //        }
    //    }
    //}

}
