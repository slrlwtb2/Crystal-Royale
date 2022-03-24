using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_holder : MonoBehaviour
{
    public Auto_attack attack;
    

    float cooldownTime;
    float activeTime;
    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyUp("space"))
                {
                    attack.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = attack.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;

                }
                else
                {
                    attack.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = attack.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

}
