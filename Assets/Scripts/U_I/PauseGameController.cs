using UnityEngine;

public class PauseGameController : MonoBehaviour
{
    public void Exit()
    {
        SceneLoadingManager.Instance.LoadLevel(0);
    }

    public void Continue()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
