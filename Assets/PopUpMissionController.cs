using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMissionController : MonoBehaviour
{
   public void GameReady()
   {
      GameManager.Instance.isCameraReadyInGame = true;
   }
}
