using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    public static GameObject instance;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] clips;

    private bool backtoscene12;
    private bool set;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //backtoscene12 = false;
        audioSource = this.GetComponent<AudioSource>();
        //audioSource.clip = clips[0];
        //audioSource.Play();
        //set = true;

        set = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu" || SceneManager.GetActiveScene().name == "RoomMenu")
        {
            if (!set)
            {
                TriggerWind(set);
                audioSource.clip = clips[0];
                audioSource.Play();
                set = true;
            }
            //if (backtoscene12 == true)
            //{
                
            //    audioSource.clip = clips[0];
            //    audioSource.Play();
            //    backtoscene12 = false;
            //}
            //return;   
        }
        if(SceneManager.GetActiveScene().name == "Origin")
        {
            if (set)
            {
                TriggerWind(set);
                set = false;
            }
        
        }
    }

    void TriggerWind(bool set)
    {
        if (set)
        {
            audioSource.clip = clips[1];
            audioSource.Play();
        }
        if (!set)
        {
            audioSource.Stop();
            //audioSource.clip = clips[0];
            //audioSource.Play();
            //backtoscene12 = true;
        }
    }

}
