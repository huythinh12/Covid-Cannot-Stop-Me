using UnityEngine;

public class CompletedButton : MonoBehaviour
{
   public void BackToMainMenu()
   {
      SceneLoadingManager.Instance.LoadLevel( 0);
   }
}
