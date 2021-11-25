using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isClearAllStage;
    public List<string> listInfected = new List<string>() ;
    public float timer;
    public bool isWin;
    public bool isFail;
    public bool isCameraReadyInGame;
    public float timeToReady;
    public float timeCountDown;
    public bool isEndTime;
    public bool isAllEnemyDefeatLV1;
    public bool isTurnOn;
    private float saveTimerPersistance;
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

    private void Start()
    {
        saveTimerPersistance = timer;
    }

    // Update is called once per frame
    void Update()
    {
        //test game mission 1
        //lose
        //heal 
        if (Input.GetKeyDown(KeyCode.J))
        {
            isFail = true;
            isEndTime = true;
        }

        //win 
        if (Input.GetKeyDown(KeyCode.K))
        {
            isEndTime = true;
            isWin = true;
        }
        if (SceneManager.GetActiveScene().buildIndex > 1 && !isTurnOn)
        {
            //default settup when game begin
            isWin = false;
            isFail = false;
            isTurnOn = true;
            isEndTime = false;
            isCameraReadyInGame = false;
            timer = saveTimerPersistance;
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitUntil(() => SceneLoadingManager.hasLoadingDone); // when fadin completed
        yield return new WaitUntil(() => isCameraReadyInGame);

        while (!isEndTime)
        {
            yield return null;
        }

        if (isWin)
        {
            yield return new WaitForSeconds(5);
            SceneLoadingManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}