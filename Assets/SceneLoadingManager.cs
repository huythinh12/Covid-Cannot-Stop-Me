using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SceneLoadingManager : MonoBehaviour
{
    public static SceneLoadingManager Instance;
    public static UnityEvent<bool> OnEventFadeIn = new UnityEvent<bool>();
    public static bool hasLoadingDone;
    public float timeToCrossFade;
    public bool isGameReady;
    public float minimumLoadTime = 10f;
    public float maximumLoadTime = 12f;
    private bool isTurnOn;
    private Slider slider;
    private int cacheIndex;
    private Stopwatch loadTimer;
    private TMP_Text loadingText;

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

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !isTurnOn)
        {
            isTurnOn = true;
            slider = GameObject.FindGameObjectWithTag("SliderLoading").GetComponent<Slider>();
            loadingText = GameObject.FindGameObjectWithTag("TextLoading").GetComponent<TMP_Text>();
            OnEventFadeIn?.Invoke(true);
            StartCoroutine(LoadAsynchronously(cacheIndex));
        }
        else if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            isTurnOn = false;
        }
    }

    public void LoadLevel(int Index)
    {
 
        Time.timeScale = 1;
        isGameReady = false;
        GameManager.Instance.isTurnOn = false;
        cacheIndex = Index;
        if (Index != 1)
        {
            Index = 1;
            StartCoroutine(DelayToStartGame(Index));
        }
        else
        {
            cacheIndex++;
            StartCoroutine(DelayToStartGame(Index));

            // StartCoroutine(LoadAsynchronously(Index));
        }
    }

    IEnumerator DelayToStartGame(int Index)
    {
        OnEventFadeIn?.Invoke(false);

        yield return new WaitForSeconds(timeToCrossFade);
        SceneManager.LoadScene(Index);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        hasLoadingDone = false;
        // sử dụng để cộng thêm tg mặc định nếu cần
        loadTimer = new Stopwatch();
        loadTimer.Start();
        var loadTimeEnd = Random.Range(minimumLoadTime, maximumLoadTime);
        float percentLoaded = 0;
        while (loadTimer.Elapsed.TotalSeconds <= loadTimeEnd)
        {
            percentLoaded = (float) (loadTimer.Elapsed.TotalSeconds / loadTimeEnd);
            slider.value = percentLoaded;
            loadingText.text = string.Format("{0:P0}", percentLoaded);

            yield return null;
        }
        isGameReady = true;

        loadTimer.Stop();
        OnEventFadeIn?.Invoke(false);

        yield return new WaitForSeconds(timeToCrossFade);

        // loading async nhưng chạy nhanh nên ko thấy hiệu ứng 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
        OnEventFadeIn?.Invoke(true);

    }

  
}