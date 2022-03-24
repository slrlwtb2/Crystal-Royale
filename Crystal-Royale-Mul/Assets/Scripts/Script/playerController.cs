using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class playerController : MonoBehaviourPunCallbacks, IDamageable
{
    public float speed = 8.0f;

    private PlayerControllerJoystick joystick;
    private Rigidbody m_Rb;
    private PlayerInfo playerInfo;
    private PhotonView pv;
    
    
    public Slider healthbar;

    private int team;

    private CameraWork _cameraWork;
    private Animator anim;

    void Awake()
    {
        // m_Rb = GetComponent<Rigidbody>();
        m_Rb = GetComponentInChildren<Rigidbody>();
        joystick = GameObject.Find("imgJoystickBg").GetComponent<PlayerControllerJoystick>();
        anim = GetComponentInChildren<Animator>();
       // pv = GetComponent<PhotonView>();
       // playerInfo = this.GetComponent<PlayerInfo>();
        healthbar.maxValue = playerInfo.current_health;
       // team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
    }

    private void Start()
    {
        //_cameraWork = this.gameObject.GetComponent<CameraWork>();
        //if (_cameraWork != null)
        //{
        //    if (pv.IsMine)
        //    {
        //        _cameraWork.OnStartFollowing();
        //    }
        //}
        //else
        //{
        //    Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        //}
    }





    void FixedUpdate()
    {
        //if (pv.IsMine && team == 0)
        //{
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

            //make player look at return value of vector3
            Quaternion targetRotation = Quaternion.LookRotation(movement);


            //Debug.Log(targetRotation.eulerAngles);

            targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);

            m_Rb.MovePosition(m_Rb.position + movement * speed * Time.fixedDeltaTime);
            m_Rb.MoveRotation(targetRotation);
            
        //}
        //if (pv.IsMine && team == 1)
        //{
            //float horizontalInput = joystick.inputHorizontal();
            //float verticalInput = joystick.inputVertical();

            //Vector3 movement = new Vector3(-horizontalInput, 0, -verticalInput).normalized;
            ////Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
            //if (movement == Vector3.zero)
            //{
            //    anim.SetFloat("Move", 0);
            //    return;
            //}
            //else
            //{
            //    anim.SetFloat("Move", 1);
            //}

            ////make player look at return value of vector3
            //Quaternion targetRotation = Quaternion.LookRotation(movement);


            ////Debug.Log(targetRotation.eulerAngles);

            //targetRotation = Quaternion.RotateTowards(
            //    transform.rotation,
            //    targetRotation,
            //    360 * Time.fixedDeltaTime);

            //m_Rb.MovePosition(m_Rb.position + movement * speed * Time.fixedDeltaTime);
            //m_Rb.MoveRotation(targetRotation);

        //}


    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (pv.IsMine)
    //    {
    //        if (collision.gameObject.tag == "Bullet")
    //        {
    //            TakeDamage(10);
    //        }
    //        if (collision.gameObject.tag == "Big_Bullet")
    //        {
    //            TakeDamage(30);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            if (other.gameObject.tag == "Bullet")
            {
                TakeDamage(10);
                Destroy(other);
            }
            if (other.gameObject.tag == "Big_Bullet")
            {
                TakeDamage(30);
            }
        }
    }



    public void TakeDamage(float damage)
    {
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        //if (!pv.IsMine) return;
        playerInfo.current_health -= damage;
        healthbar.value = playerInfo.current_health;
        if(playerInfo.current_health <= 0)
        {
            healthbar.value = 0;
            //Die();
        }
    }
                                       
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(playerInfo.current_health);
    //    }
    //    else if (stream.IsReading)
    //    {
    //        playerInfo.current_health = (float)stream.ReceiveNext();
    //    }
    //}
}