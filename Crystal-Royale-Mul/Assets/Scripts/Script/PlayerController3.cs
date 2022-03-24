using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerController3 : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField]
    private HeroSO heroData;
    private GameObject avatar;
    public GameObject indicator;
    private PlayerControllerJoystick joystick;
    private Rigidbody m_Rb;
    //[HideInInspector]
    public Animator anim;

    public GameObject model;

    
    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private AudioClip bclip;

    private AudioSource audioSource;

    public Transform sender;

    private bool getheal;
    private bool getsub;
    private float interval = 1f;
    private float elapse;

    private float sl;

    private CameraWork _cameraWork;
    private PlayerInfo playerInfo;
    public Slider healthbar;
    //private PhotonView pv;
    private int team;

    private float cooldown;

    private int bcd;

    public bool changeable;

    public bool moveable;

    public float speedcache;

    public float spd;
    [HideInInspector]
    public float range;

    void awake()
    {

        //pv = GetComponent<PhotonView>();


        //pv = this.photonView;
        //moveable = true;
        //team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        //pv = this.GetComponent<PhotonView>();

        //healthbar.maxValue = playerInfo.current_health;

    }

    private void Start()
    {
        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        //model = this.transform.Find("model").gameObject;
        anim = GetComponentInChildren<Animator>();
        playerInfo = this.GetComponent<PlayerInfo>();
        //this.gameObject.GetComponentInChildren<ParticleSystem>().emission;
        var emission = this.gameObject.GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = false;
        healthbar.maxValue = playerInfo.current_health;
        m_Rb = GetComponentInChildren<Rigidbody>();
        _cameraWork = this.GetComponent<CameraWork>();
        changeable = true;
        bcd = 3;
        audioSource = GetComponent<AudioSource>();
        //ender = this.gameObject.transform;
        //Instance hero's avatar then set it child in player object//
        //if (photonView.IsMine)
        //{
        //avatar = Instantiate(heroData.model);
        //avatar = Instantiate(heroData.model);
        //avatar.transform.SetParent(gameObject.transform);
        //avatar.transform.localPosition = -1 * Vector3.up;
        //avatar.transform.localRotation = Quaternion.identity;
        //}

        joystick = GameObject.Find("imgJoystickBg").GetComponent<PlayerControllerJoystick>();
        //anim = GetComponentInChildren<Animator>();
        indicator.SetActive(false);
        //Get Hero's data//
        //Debug.Log(anim);
        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }

        moveable = true;
        spd = heroData.speed;
        range = heroData.AttackRange;
        speedcache = heroData.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "heal")
            {
                getheal = true;
                //Healing(other.GetComponent<AreaCard>().card.heal);
                //StartCoroutine(healing()); 
            }
            if(other.gameObject.tag == "decrease")
            {
                getsub = true;
                //StartCoroutine(decrease());
            }
            if (other.gameObject.tag == "slow")
            {
                sl = other.gameObject.GetComponent<AreaCard>().card.duration;
                spd = 3;
                //StartCoroutine(decrease());
            }
            if (other.gameObject.tag == "Grass"&&changeable)
            {
                Color color = other.GetComponent<Grass_New>().matt.color;
                color.a = 0f;
                other.GetComponent<Grass_New>().matt.color = color;
                photonView.RPC("ChangeLayer", RpcTarget.All, other.gameObject.name);
                this.GetComponent<CameraWork>().layer = other.gameObject.name;
                //photonView.RPC("ChangeLayersRecursively", RpcTarget.All,this.gameObject.GetPhotonView().ViewID,layer);
            }
            if(other.gameObject.tag == "hammer"&&other.gameObject.GetComponent<Colliderhit>().team != this.team)
            {
                sl = other.gameObject.GetComponentInParent<selfdes>().activetime;
                spd = 0;
            }
            if (other.gameObject.tag == "fireg" && other.gameObject.GetComponent<Colliderhit>().team != this.team)
            {
                getsub = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine)
        {
            if(other.gameObject.tag == "heal"&&getheal)
            {
                elapse += Time.fixedDeltaTime;
                if(elapse > interval)
                {
                    Healing(other.GetComponent<AreaCard>().card.heal);
                    elapse = 0;
                }
                
                //StartCoroutine(healing());
                //getheal = true;
            }
            if(other.gameObject.tag == "decrease"&&getsub)
            {
                elapse += Time.fixedDeltaTime;
                if (elapse > interval)
                {
                    TakeDamage(other.GetComponent<AreaCard>().card.damage);
                    elapse = 0;
                }
                
                //StartCoroutine(decrease());
                //getsub = true;
            }
            if(other.gameObject.tag == "fireg" && other.gameObject.GetComponent<Colliderhit>().team != this.team&&getsub)
            {
                elapse += Time.fixedDeltaTime;
                if (elapse > interval)
                {
                    TakeDamage(other.GetComponent<Colliderhit>().damage);
                    elapse = 0;
                }
            }
            //if (other.gameObject.tag == "slow"&& other.gameObject.GetComponent<AreaCard>().reset)
            //{
            //    spd = speedcache;
            //    //StartCoroutine(decrease());
            //}

            if(other.gameObject.tag == "Grass" && changeable)
            {
                Color color = other.GetComponent<Grass_New>().matt.color;
                color.a = 0f;
                other.GetComponent<Grass_New>().matt.color = color;
                photonView.RPC("ChangeLayer", RpcTarget.All, other.gameObject.name);
                this.GetComponent<CameraWork>().layer = other.gameObject.name;
                //photonView.RPC("ChangeLayersRecursively", RpcTarget.All,this.gameObject.GetPhotonView().ViewID,layer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Grass")
            {
                photonView.RPC("ChangeLayer", RpcTarget.All, "Default");
                other.GetComponent<Grass_New>().matt.color = other.GetComponent<Grass_New>().color_temp;
                //photonView.RPC("ChangeLayersRecursively", RpcTarget.All, this.gameObject.GetPhotonView().ViewID, "Default");
            }
            if (other.gameObject.tag == "heal")
            {
                getheal = false;
                elapse = 0;
            }
            if (other.gameObject.tag == "decrease")
            {
                getsub = false;
                elapse = 0;
            }
            if (other.gameObject.tag == "fireg")
            {
                getsub = false;
                elapse = 0;
            }
            //if (other.gameObject.tag == "slow")
            //{
            //spd = speedcache;
            //StartCoroutine(decrease());
            //}
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown("space"))
            {
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
                indicator.SetActive(true);
            }
            else
            {
                if (Input.GetKeyUp("space"))
                {
                    //audioSource.Stop();
                    indicator.SetActive(false);
                }
            }
            if (sl > 0)
            {
                sl -= Time.deltaTime;
                if (sl <= 0)
                {
                    spd = speedcache;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (photonView.IsMine && team == 0)
        {
            if (moveable == true)
            {
                float horizontalInput = joystick.inputHorizontal();
                float verticalInput = joystick.inputVertical();
                Vector3 movement = new Vector3(-horizontalInput, 0, -verticalInput).normalized;

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
            //m_Rb.MovePosition(m_Rb.position + movement * spd * Time.fixedDeltaTime);
            //m_Rb.velocity = movement * spd;
            //m_Rb.velocity = new Vector3(movement.x, m_Rb.velocity.y, movement.z) * spd;
            m_Rb.velocity = new Vector3(movement.x * spd, m_Rb.velocity.y, movement.z * spd);
            m_Rb.MoveRotation(targetRotation);
            }
        }
        if (photonView.IsMine && team == 1)
        {
            if (moveable == true)
            {
            float horizontalInput = joystick.inputHorizontal();
            float verticalInput = joystick.inputVertical();
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

            if (movement == Vector3.zero)
            {
                //Debug.Log("here");
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
            //m_Rb.velocity = movement * spd;
            //m_Rb.MovePosition(m_Rb.position + movement * spd * Time.fixedDeltaTime);
            //m_Rb.velocity = new Vector3(movement.x, m_Rb.velocity.y, movement.z) * spd;
            m_Rb.velocity = new Vector3(movement.x * spd, m_Rb.velocity.y, movement.z * spd);
            m_Rb.MoveRotation(targetRotation);

            }
        }

    }

    public void TakeDamage(float damage)
    {
        if (this.gameObject.GetComponent<PlayerInfo>().getkill)
        {
            return;
        }
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    public void Healing(float heal)
    {
        photonView.RPC("RPC_Healing", RpcTarget.All, heal);
    }



    [PunRPC]
    void RPC_Healing(float heal)
    {
        if (playerInfo.current_health >= 100)
        {
            return;
        }
        playerInfo.current_health += heal;
        healthbar.value = playerInfo.current_health;
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        //if (!pv.IsMine) return;
        
        if (playerInfo.current_health <= 0)
        {
            playerInfo.isdead = true;
            playerInfo.getkill = true;
            //healthbar.value = 0;
            return;
            //Die();
        }
        else
        {
            this.audioSource.PlayOneShot(bclip);
            var emission = this.gameObject.GetComponentInChildren<ParticleSystem>().emission;
            emission.enabled = true;
            playerInfo.current_health -= damage;
            healthbar.value = playerInfo.current_health;
            StartCoroutine(stopbleed());
            //this.gameObject.GetComponentInChildren<ParticleSystem>().Pause();
        }
      
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerInfo.current_health);
            stream.SendNext(playerInfo.isdead);
            stream.SendNext(playerInfo.getkill);
        }
        else if (stream.IsReading)
        {
            playerInfo.current_health = (float)stream.ReceiveNext();
            playerInfo.isdead = (bool)stream.ReceiveNext();
            playerInfo.getkill = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void ChangeLayersRecursively(int viewid,string name)
    {
        sender = PhotonView.Find(viewid).transform;
        foreach (Transform child in sender)
        {
            child.gameObject.layer = LayerMask.NameToLayer(name);
            photonView.RPC("ChangeLayersRecursively", RpcTarget.All,child.gameObject.GetComponentInParent<PhotonView>().ViewID, name);
            //ChangeLayersRecursively(child.gameObject.GetPhotonView().ViewID, name);
        }
    }

    [PunRPC]
    public void ChangeLayer(string name)
    {
        //if (this.gameObject.transform.Find("Fire_indicator") != null)
        //{
        //    //gameObject.layer = LayerMask.NameToLayer("Water");
        //    return;
        //}
        //else
        //{
            gameObject.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.name == "Fire_indicator")
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Water");
                }
                else
                {
                    child.gameObject.layer = LayerMask.NameToLayer(name);
                }
                if (child.transform.childCount != 0)
                {
                    foreach (Transform child2 in child)
                    {
                        child2.gameObject.layer = LayerMask.NameToLayer(name);
                        if (child2.transform.childCount != 0)
                        {
                            foreach (Transform child3 in child2)
                            {
                                child3.gameObject.layer = LayerMask.NameToLayer(name);
                                if (child3.transform.childCount != 0)
                                {
                                    foreach (Transform child4 in child3)
                                    {
                                        child4.gameObject.layer = LayerMask.NameToLayer(name);
                                        if (child4.transform.childCount != 0)
                                        {
                                            foreach (Transform child5 in child4)
                                            {
                                                child5.gameObject.layer = LayerMask.NameToLayer(name);
                                                if (child5.transform.childCount != 0)
                                                {
                                                    foreach (Transform child6 in child5)
                                                    {
                                                        child6.gameObject.layer = LayerMask.NameToLayer(name);
                                                        if (child6.transform.childCount != 0)
                                                        {
                                                            foreach (Transform child7 in child6)
                                                            {
                                                                child7.gameObject.layer = LayerMask.NameToLayer(name);
                                                                if (child7.transform.childCount != 0)
                                                                {
                                                                    foreach (Transform child8 in child7)
                                                                    {
                                                                        child8.gameObject.layer = LayerMask.NameToLayer(name);
                                                                        if (child8.transform.childCount != 0)
                                                                        {
                                                                            foreach (Transform child9 in child8)
                                                                            {
                                                                                child9.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                if (child9.transform.childCount != 0)
                                                                                {
                                                                                    foreach (Transform child10 in child9)
                                                                                    {
                                                                                        child10.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                        if (child10.transform.childCount != 0)
                                                                                        {
                                                                                            foreach (Transform child11 in child10)
                                                                                            {
                                                                                                child11.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                                if (child11.transform.childCount != 0)
                                                                                                {
                                                                                                    foreach (Transform child12 in child11)
                                                                                                    {
                                                                                                        child12.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                                        if (child12.transform.childCount != 0)
                                                                                                        {
                                                                                                            foreach (Transform child13 in child12)
                                                                                                            {
                                                                                                                child13.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                                                if (child13.transform.childCount != 0)
                                                                                                                {
                                                                                                                    foreach (Transform child14 in child13)
                                                                                                                    {
                                                                                                                        child14.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                                                        if (child14.transform.childCount != 0)
                                                                                                                        {
                                                                                                                            foreach (Transform child15 in child14)
                                                                                                                            {
                                                                                                                                child15.gameObject.layer = LayerMask.NameToLayer(name);
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        //}
    }

    IEnumerator healing()
    {
        getheal = false;
        yield return new WaitForSeconds(3);        
        //yield return new WaitForSeconds(5);
    }

    IEnumerator decrease()
    {
        getsub = false;
        yield return new WaitForSeconds(3);
        //yield return new WaitForSeconds(5);
    }

    IEnumerator stopbleed()
    {
        yield return new WaitForSeconds(1);
        var emission = this.gameObject.GetComponentInChildren<ParticleSystem>().emission;
        emission.enabled = false;
    }

    //IEnumerator healing(float heal)
    //{
    //    yield return new WaitForSeconds(1);

    //    getheal = false;
    //    //yield return new WaitForSeconds(5);
    //}

    //IEnumerator decrease(float damage)
    //{
    //    yield return new WaitForSeconds(1);
    //    TakeDamage(damage);
    //    getsub = false;
    //    //yield return new WaitForSeconds(5);
    //}
}

