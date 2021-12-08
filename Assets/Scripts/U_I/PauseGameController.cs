using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    public static bool isClickedContinue ;
    public void Exit()
    {
        SceneLoadingManager.Instance.LoadLevel(0);
    }

    public void Continue()
    {
        //isClickedContinue = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        CinemachineManager.isStopCameraThird = false;
    }
}
