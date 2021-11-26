using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private void Start()
    {
        Invoke("StopEffect",3f);
    }

 

    private void StopEffect()
    {
        GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject);

    }
}