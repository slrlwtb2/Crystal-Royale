using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    private Quaternion startrot;
    // Start is called before the first frame update
    void Start()
    {
        startrot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startrot;
    }
}
