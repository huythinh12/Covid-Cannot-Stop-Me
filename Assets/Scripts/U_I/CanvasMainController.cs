using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainController : MonoBehaviour
{
    public GameObject pauseGame;
    public GameObject settingGame;
    public GameObject missionCompletedStamp;
    public GameObject popUpMissionContent;
    private bool isTurnOn;
    private bool isTurnOn2;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isCameraReadyInGame && !isTurnOn &&
            SceneManager.GetActiveScene().buildIndex != 5)
        {
            isTurnOn = true;
            popUpMissionContent.SetActive(true);
        }

        if (GameManager.Instance.isWin && !isTurnOn2 && SceneManager.GetActiveScene().buildIndex != 5)
        {
            isTurnOn2 = true;
            popUpMissionContent.SetActive(!popUpMissionContent.activeInHierarchy);
            missionCompletedStamp.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                popUpMissionContent.SetActive(!popUpMissionContent.activeInHierarchy);
            }
        }


        if (!pauseGame.activeInHierarchy && !settingGame.activeInHierarchy && !GameManager.Instance.isFail)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.isFail)
        {
            pauseGame.SetActive(!pauseGame.activeInHierarchy);
            if (pauseGame.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}