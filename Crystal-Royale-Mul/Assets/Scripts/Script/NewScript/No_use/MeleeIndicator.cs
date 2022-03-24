using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeIndicator : MonoBehaviour
{
    //https://www.youtube.com/watch?v=Hi61o1_duwo
    // Start is called before the first frame update
    [SerializeField]
    Renderer indicator;
    void Start()
    {
        indicator = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey("space"))
        {
            DrawLine();
        }
        else { indicator.enabled = false; }
    }

    public void DrawLine()
    {

        indicator.enabled = true;
    }
}
