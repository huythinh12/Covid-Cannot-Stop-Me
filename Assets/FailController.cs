using UnityEngine;
using UnityEngine.SceneManagement;

public class FailController : MonoBehaviour
{
    public void Restart()
    {
        gameObject.SetActive(false);
        SceneLoadingManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        SceneLoadingManager.Instance.LoadLevel(0);
    }
}