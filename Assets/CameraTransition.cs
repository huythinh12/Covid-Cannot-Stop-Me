using UnityEngine;
using UnityEngine.Events;

public class CameraTransition : MonoBehaviour
{
    public UnityEvent<bool> OnCameraReady;
    private bool isGameReady;
    private bool isTurnOn;
    private void Start()
    {
        OnCameraReady?.Invoke(false);
    }

    public void CameraReady()
    {
        OnCameraReady?.Invoke(true);
    }

    private void Update()
    {
        if (GameManager.Instance.isCameraReadyInGame && !isTurnOn)
        {
            gameObject.SetActive(false);
            isTurnOn = true;
        }
    }
}