using System;
using UnityEngine;

public class CanvasMainController : MonoBehaviour
{
    public GameObject pauseGame;
    public GameObject popUpMissionContent;
    private bool isTurnOn;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isCameraReadyInGame&&!isTurnOn)
        {
            isTurnOn = true;
            popUpMissionContent.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            popUpMissionContent.SetActive(!popUpMissionContent.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CinemachineManager.isStopCameraThird = true;
            pauseGame.SetActive(!pauseGame.activeInHierarchy);
            if (pauseGame.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
            else
            {
                CinemachineManager.isStopCameraThird = false;
                Time.timeScale = 1;
            }
        }
    }
}
