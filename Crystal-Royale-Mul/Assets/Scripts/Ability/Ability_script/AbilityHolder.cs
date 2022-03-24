using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class AbilityHolder : MonoBehaviourPun
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    [SerializeField]
    private int order;

    [SerializeField]
    private GameObject s;
    [SerializeField]
    private GameObject c;


    private AudioSource audioSource;

    public AudioClip clip;    

    enum AbilityState 
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;
    public KeyCode key;

    private void Start()
    {
        if (photonView.IsMine)
        {
            if (order == 1)
            {
                audioSource = this.GetComponent<AudioSource>();
                s = GameObject.FindGameObjectWithTag("skill1");
                s.GetComponent<Text>().text = ability.name;
                c = GameObject.FindGameObjectWithTag("cd1");
                c.GetComponent<Text>().text = "Ready";
            }
            if (order == 2)
            {
                audioSource = this.GetComponent<AudioSource>();
                s = GameObject.FindGameObjectWithTag("skill2");
                s.GetComponent<Text>().text = ability.name;
                c = GameObject.FindGameObjectWithTag("cd2");
                c.GetComponent<Text>().text = "Ready";
            }
        }
    }


    void Update()
    {
        if (photonView.IsMine&&(order==1||order==2))
        {
            switch (state)
            {
                case AbilityState.ready:
                    if (Input.GetKeyDown(key))
                    {
                        //do skill
                        if (ability.name == "Berserk" || ability.name == "Steroid" || ability.name == "Sky strike" || ability.name == "Dodge" || ability.name == "Dash" || ability.name == "Jump"||ability.name == "FB")
                        {
                            GetComponent<PlayerController3>().changeable = true;
                        }
                        else
                        {
                            GetComponent<PlayerController3>().changeable = false;
                        }
                        if (order == 1 || order == 2)
                        {
                            audioSource.clip = clip;
                            audioSource.Play();
                        }
                        ability.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = ability.activeTime;
                    }
                    break;
                case AbilityState.active:
                    //audioSource.Stop();
                    if (activeTime > 0)
                    {
                        //  Debug.Log(activeTime);
                        activeTime -= Time.deltaTime;
                        c.GetComponent<Text>().text = "Using";
                    }
                    else
                    {
                        // Debug.Log(activeTime);
                        if (order == 1 || order == 2)
                        {
                            audioSource.Stop();
                        }
                        GetComponent<PlayerController3>().changeable = true;
                        ability.BeginCooldown(gameObject);
                        state = AbilityState.cooldown;
                        cooldownTime = ability.cooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    //audioSource.Stop();
                    if (cooldownTime > 0)
                    {
                        //   Debug.Log(cooldownTime);
                        cooldownTime -= Time.deltaTime;
                        c.GetComponent<Text>().text = cooldownTime.ToString("0");
                    }
                    else
                    {
                        //   Debug.Log(cooldownTime);
                        state = AbilityState.ready;
                        c.GetComponent<Text>().text = "Ready";
                    }
                    break;
            }
        }
    }


    }
