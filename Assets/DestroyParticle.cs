using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{

    private float timeFollow = 2f;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent = other.transform.parent;
            Invoke("StopEffect",timeFollow);
        }
    }

    private void StopEffect()
    {
        GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, timeFollow);

    }
}