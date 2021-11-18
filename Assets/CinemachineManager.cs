using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    public CinemachineFreeLook cinemachineFreeLook;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        GameManager.OnGameReady.AddListener(RecievedGameReady);
    }

    private void RecievedGameReady()
    {
        cinemachineFreeLook.enabled = true;
    }
    private void OnDisable()
    {
        GameManager.OnGameReady.RemoveListener(RecievedGameReady);
    }
}
