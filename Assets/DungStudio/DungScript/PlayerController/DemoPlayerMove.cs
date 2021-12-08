using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerMove : MonoBehaviour
{
    //Reference
    [SerializeField] private float moveSpeed;
    //[SerializeField] private GameObject playerParticleBound;

    private float xAxis;
    private float zAxis;

    private void FixedUpdate()
    {
        PlayerMove();
    }
   
    //Collider
    // private void OnTriggerEnter(Collider other)
    // {
    //     playerParticleBound.SetActive(true);
    //     Debug.Log("Trigger Particle Active");
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     playerParticleBound.SetActive(false);
    //     Debug.Log("Trigger Particle Deactive");
    // }

    //My Method
    private void PlayerMove()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");


        transform.Translate(Vector3.right * xAxis * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * zAxis * moveSpeed * Time.deltaTime);
    }

}
