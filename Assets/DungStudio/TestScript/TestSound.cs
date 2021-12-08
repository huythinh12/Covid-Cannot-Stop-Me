using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioSource audioWhenTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {audioWhenTrigger.Play();}
    }

    private void OnTriggerExit(Collider other)
    {
        audioWhenTrigger.Stop();
    }
}