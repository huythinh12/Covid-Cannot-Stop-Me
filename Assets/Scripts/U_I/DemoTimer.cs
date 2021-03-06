using UnityEngine;
using Menu;
public class DemoTimer : MonoBehaviour
{
    [SerializeField] private Timer ClockTimer;
    [SerializeField] private int NumberTime;
    private bool isTurnOn;

    private void Update()
    {
        if (GameManager.Instance != null && !isTurnOn)
        {
            if (GameManager.Instance.isCameraReadyInGame)
            {
                NumberTime = (int) GameManager.Instance.timer;

                StartCountingClock();
                isTurnOn = true;
            }
        }

        PauseGame();
    }

    private void StartCountingClock()
    {
        ClockTimer
            .SetDuration(NumberTime)
            .OnBegin(() => Debug.Log("Timer started"))
            .OnChange((remainingSeconds) => Debug.Log("Timer changed : " + remainingSeconds))
            .OnEnd(() => GameManager.Instance.isEndTime = true)
            .OnPause(IsPaused => Debug.Log("----Paused : " + IsPaused))
            .Begin();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            ClockTimer.SetPause(!ClockTimer.IsPause);
        }
        else if (!PauseMenu.GameIsPaused)
        {
            ClockTimer.SetPause(false);
        }
    }
}