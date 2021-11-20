using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
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
        isClickedContinue = true;
        CinemachineManager.isStopCameraThird = false;
    }
}
