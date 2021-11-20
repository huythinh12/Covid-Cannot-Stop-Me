using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    public GameObject cameraThird;
    public GameObject cameraMain;
    private bool isTurnOn;

    private void Update()
    {
        if (GameManager.Instance != null)
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                cameraThird.SetActive(true);
                cameraMain.SetActive(true);
                isTurnOn = true;
            }
    }
}