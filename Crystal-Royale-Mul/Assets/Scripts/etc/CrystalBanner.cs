using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalBanner : MonoBehaviour
{
    GameObject rc;
    GameObject bc;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (rc != null && bc != null)
        {
            if (this.name == "RCHealth")
            {
                this.GetComponent<Text>().text = rc.GetComponent<Crystal>().health.ToString("0");
            }
            if (this.name == "BCHealth")
            {
                this.GetComponent<Text>().text = bc.GetComponent<Crystal>().health.ToString("0");
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        rc = GameObject.FindGameObjectWithTag("RedCrystal");
        bc = GameObject.FindGameObjectWithTag("BlueCrystal");
    }
}
