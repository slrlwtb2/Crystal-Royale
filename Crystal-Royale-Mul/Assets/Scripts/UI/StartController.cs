using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    //public GameObject[] login;
    //public GameObject[] register;
    public GameObject[] nickname;

    public GameObject play;
    public GameObject quit;
    public GameObject credit;
    public GameObject htp;

    public GameObject back1;
    public GameObject back2;
    public GameObject back3;

    public void OnNewAccount()
    {
        //foreach(GameObject g in login)
        //{
        //    g.SetActive(false);
        //}
        //foreach(GameObject g in register)
        //{
        //    g.SetActive(true);
        //}
        //foreach (GameObject g in nickname)
        //{
        //    g.SetActive(false);
        //}
    }

    public void OnRegistered()
    {
        //foreach (GameObject g in login)
        //{
        //    g.SetActive(true);
        //}
        //foreach (GameObject g in register)
        //{
        //    g.SetActive(false);
        //}
        //foreach (GameObject g in nickname)
        //{
        //    g.SetActive(false);
        //}
    }

    public void OnNickName()
    {

        play.SetActive(false);
        quit.SetActive(false);
        htp.SetActive(false);
        credit.SetActive(false);
        //foreach (GameObject g in login)
        //{
        //    g.SetActive(false);
        //}
        //foreach (GameObject g in register)
        //{
        //    g.SetActive(false);
        //}
        foreach (GameObject g in nickname)
        {
            g.SetActive(true);
        }
    }

    public void On_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
    public void On_Credit()
    {
        back2.SetActive(true);
        play.SetActive(false);
        quit.SetActive(false);
        htp.SetActive(false);
        credit.SetActive(false);
    }

    public void On_HTP()
    {
        back3.SetActive(true);
        play.SetActive(false);
        quit.SetActive(false);
        htp.SetActive(false);
        credit.SetActive(false);
    }

    public void back_1()
    {
        foreach (GameObject g in nickname)
        {
            g.SetActive(false);
        }
        play.SetActive(true);
        quit.SetActive(true);
        //htp.SetActive(true);
        //credit.SetActive(true);
    }

    public void back_2()
    {
        back2.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);
        htp.SetActive(true);
        credit.SetActive(true);
    }

    public void back_3()
    {
        back3.SetActive(false);
        play.SetActive(true);
        quit.SetActive(true);
        htp.SetActive(true);
        credit.SetActive(true);
    }
}
