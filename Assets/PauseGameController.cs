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
        Time.timeScale = 1;
        CinemachineManager.isStopCameraThird = false;
    }
}
