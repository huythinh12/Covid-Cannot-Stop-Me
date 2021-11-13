using System;
using System.Security.Cryptography;
using Player;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int throwForce;
    
    // [SerializeField] private GameObject throwPoint;
    [SerializeField] private GameObject explosionFX;
    private Vector3 aimDir;
    private Rigidbody rbBall;
    private GameObject throwPoint;

    void Start()
    {
        throwPoint = GameObject.FindGameObjectWithTag("ThrowPoint");
        rbBall = GetComponent<Rigidbody>();
        transform.parent = throwPoint.transform;
        transform.position = throwPoint.transform.position;
        
    }

    public void ReleaseMe()
    {
        transform.parent = null;
        rbBall.useGravity = true;
        transform.rotation = throwPoint.transform.rotation;
        rbBall.AddForce(AimController.aimDir * throwForce, ForceMode.Impulse);
        Invoke("RemoveDelay",5);
    }

    private void RemoveDelay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}