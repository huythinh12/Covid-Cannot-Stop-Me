using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanVasLookAtCamera : MonoBehaviour
{
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
         transform.LookAt(transform.position + cam.forward);
    }
}
