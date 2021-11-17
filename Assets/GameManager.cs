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
        loadTimer = new Stopwatch();
        loadTimer.Start();
        
        while (loadTimer.Elapsed.TotalSeconds <= timer)
        {
            yield return null;
        }

        Time.timeScale = 0;
        SceneLoadingManager.Instance.LoadLevel(0);
    }
   
}
