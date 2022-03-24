using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField]
    private HeroSO heroData;
    [SerializeField]
    private GameObject avatar;
    private PlayerControllerJoystick joystick;
    private Rigidbody m_Rb;
    public Animator anim;
    public bool moveable;
    public float spd;
    public float range;
    public float coolDown;
    void awake()
    {
        

    }
    private void Start()
    {
        //Instance hero's avatar then set it child in player object//
        avatar = Instantiate(heroData.model);
        avatar.transform.SetParent(gameObject.transform);
        avatar.transform.localPosition = -1 * Vector3.up;
        avatar.transform.localRotation = Quaternion.identity;
        m_Rb = GetComponentInChildren<Rigidbody>();
        joystick = GameObject.Find("imgJoystickBg").GetComponent<PlayerControllerJoystick>();
        anim = GetComponentInChildren<Animator>();

        //Get Hero's data//
        moveable = true;
        spd = heroData.speed;
        range = heroData.AttackRange;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            if (moveable == true) 
            {
                moveable = false; 
                anim.SetFloat("Move", 0); 
                anim.SetFloat("Ulti", 1); 

            }
            else { moveable = true; Debug.Log(moveable); anim.SetFloat("Ulti", 0); }
        }
    }
    void FixedUpdate()
    {
        if (moveable == true)
        {
            float horizontalInput = joystick.inputHorizontal();
            float verticalInput = joystick.inputVertical();
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (movement == Vector3.zero)
            {
                anim.SetFloat("Move", 0);
                return;
            }
            else
            {
                anim.SetFloat("Move", 1);
            }
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
            m_Rb.MovePosition(m_Rb.position + movement * spd * Time.fixedDeltaTime);
            m_Rb.MoveRotation(targetRotation);

        }
        
    }

}