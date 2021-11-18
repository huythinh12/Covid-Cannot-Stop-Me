using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timer;
    public bool isWin;
    public bool isCameraReadyInGame;
    public float timeToReady;
    
    private bool isTurnOn;
    private Stopwatch loadTimer;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !isTurnOn)
        {
            isTurnOn = true;
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        var cameraTransition = FindObjectOfType<CameraTransition>();
        yield return new WaitUntil(() => SceneLoadingManager.hasLoadingDone);
        cameraTransition.OnCameraReady.AddListener(ReceivedCameraReadyEvent);
        yield return new WaitUntil(() => isCameraReadyInGame);

        loadTimer = new Stopwatch();
        loadTimer.Start();
        while (loadTimer.Elapsed.TotalSeconds <= timer)
        {
            yield return null;
        }

        Time.timeScale = 0;
        cameraTransition.OnCameraReady.RemoveListener(ReceivedCameraReadyEvent);

        SceneLoadingManager.Instance.LoadLevel(0);
    }

    private void ReceivedCameraReadyEvent(bool isCameraReady)
    {
        isCameraReadyInGame = isCameraReady;
    }
    
}