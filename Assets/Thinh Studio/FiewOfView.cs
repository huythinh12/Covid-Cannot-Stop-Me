using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiewOfView : MonoBehaviour
{
    public Transform anotherTarget;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    print( "khoảng cách giữa 2 object :" + Vector3.Distance(transform.position,anotherTarget.position));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,anotherTarget.position);
    }
}