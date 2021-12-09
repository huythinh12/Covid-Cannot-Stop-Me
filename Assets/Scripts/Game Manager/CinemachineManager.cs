using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
   
    public GameObject cameraMain;
    private bool isTurnOn, isTurnOn2;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
            {
                cameraMain.SetActive(true);
                isTurnOn = true;
            }
        }
    }
}