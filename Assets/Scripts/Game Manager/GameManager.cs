using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int countOfDead;
    public int countOfHeal;
    public bool isClearAllStage;
    public List<string> listInfected = new List<string>();
    public float timer;
    public bool isWin;
    public bool isFail;
    public bool isCameraReadyInGame;
    public float timeCountDown;
    public bool isEndTime;
    public static bool isTurnOn;
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

        //test fail
        if (Input.GetKeyDown(KeyCode.R))
        {
            isFail = true;
        }

        //test win
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isEndTime = true;
            listInfected.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Mission2Controller.numberVaccineInTown = 5;
        
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Mission3Controller.numberVirusDefeat = 5;
          
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Mission4Controller.isBossDead = true;
         
        }
        
        //set cursor mouse default
        if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (SceneManager.GetActiveScene().buildIndex > 1 && !isTurnOn)
        {
            Mission2Controller.numberVaccineInTown = 0;
            Mission3Controller.numberVirusDefeat = 0;
            Mission4Controller.isBossDead = false;

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
        yield return new WaitUntil(() => isWin);
        yield return new WaitForSeconds(2.5f);
        SceneLoadingManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}