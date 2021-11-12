using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int throwForce;
        
    [SerializeField] private GameObject parentBone;

    [SerializeField] private Rigidbody ballRigidbody;
    // [SerializeField] private Vector3 lastPosition;
    // [SerializeField] private Vector3 curveVelocity;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = parentBone.transform;
        ballRigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReleaseMe()
    {
        transform.parent = null;
        ballRigidbody.useGravity = true;
        transform.rotation = parentBone.transform.rotation;
        ballRigidbody.AddForce(Vector3.forward * throwForce,ForceMode.Impulse);
        print("transfrom:"+transform.forward);
        print("Vector3:"+Vector3.forward);
    }
}
