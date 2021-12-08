using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseGameUI;
        private static bool GameIsPaused = false;

        // Update is called once per frame
        void Update()
        {
            PauseMenuController();
        }
        public void BackToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
        public void ResumeGame()
        {
            pauseGameUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        public void PauseGame()
        {
            pauseGameUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        private void PauseMenuController()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
}