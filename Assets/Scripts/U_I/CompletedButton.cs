using UnityEngine;

public class CompletedButton : MonoBehaviour
{
   public void BackToMainMenu()
   {
      SceneLoadingManager.Instance.LoadLevel( 0);
      if (GameManager.Instance != null)
      {
       Invoke("SetDefaultResultDelay",3f);
      }
   }

   private void SetDefaultResultDelay()
   {
      GameManager.Instance.countOfDead = 0;
      GameManager.Instance.countOfHeal = 0;
   }
}
