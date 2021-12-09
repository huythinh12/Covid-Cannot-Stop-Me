using DG.Tweening;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private bool isTurnOn,isTurnOn2;
    public GameObject cameraThird;
    public Transform objectTarget;

    private void Update()
    {
        if (SceneLoadingManager.hasLoadingDone && !isTurnOn)
        {
            cameraThird.SetActive(false);
            gameObject.SetActive(true);
            var timeToReady = 3;
            if (transform != null)
                transform.DOMove(objectTarget.position, timeToReady);
            Invoke("CameraReady", 3.5f);
            isTurnOn = true;
        }
    }

    private void CameraReady()
    {
        GameManager.Instance.isCameraReadyInGame = true;
        gameObject.SetActive(false);
    }
}