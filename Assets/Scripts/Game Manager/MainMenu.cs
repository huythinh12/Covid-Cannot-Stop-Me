using UnityEngine;
using UnityEngine.SceneManagement;
namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
          SceneLoadingManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
          // SceneLoadingManager.Instance.LoadLevel(3);
        }

   
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}