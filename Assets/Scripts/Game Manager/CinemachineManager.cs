using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    public GameObject cameraThird;
    public GameObject cameraMain;
    public static bool isStopCameraThird;
    private bool isTurnOn;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                cameraThird.SetActive(true);
                cameraMain.SetActive(true);
                isTurnOn = true;
            }
        }

        if (isStopCameraThird)
        {
            cameraThird.SetActive(false);
        }
        else
        {
            cameraThird.SetActive(true);
        }
          
    }
}