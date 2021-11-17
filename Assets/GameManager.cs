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
    public bool isGameReady;
    public UnityEvent OnGameReady;
    
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && isTurnOn)
        {
            isTurnOn = false;
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        loadTimer = new Stopwatch();
        loadTimer.Start();
        while (loadTimer.Elapsed.TotalSeconds <= timer)
        {
            print(loadTimer.Elapsed.TotalSeconds + " second count down");
            yield return null;
        }

        print("het thoi gina r ne");
        Time.timeScale = 0;
        
        print("xin chao timer stop");
    }
   
}
