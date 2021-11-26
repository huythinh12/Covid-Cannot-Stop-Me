using DG.Tweening;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private bool isTurnOn;
    public Transform objectTarget;

    private void Update()
    {
        if (SceneLoadingManager.hasLoadingDone && !isTurnOn)
        {
            var timeToReady = 3;
            if (transform != null)
                transform.DOMove(objectTarget.position, timeToReady);
            Invoke("CameraReady", timeToReady);
            isTurnOn = true;
        }
    }

    private void CameraReady()
    {
        GameManager.Instance.isCameraReadyInGame = true;
        gameObject.SetActive(false);
    }
}