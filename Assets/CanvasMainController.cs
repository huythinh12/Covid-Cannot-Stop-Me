using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainController : MonoBehaviour
{
    public GameObject pauseGame;

    public GameObject popUpMissionContent;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isCameraReadyInGame)
        {
            popUpMissionContent.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CinemachineManager.isStopCameraThird = true;
            pauseGame.SetActive(true);
        }
    }
}
