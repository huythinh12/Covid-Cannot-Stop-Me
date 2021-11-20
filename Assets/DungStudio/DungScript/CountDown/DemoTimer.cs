using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTimer : MonoBehaviour
{
    [SerializeField] private Timer ClockTimer;
    [SerializeField] private int NumberTime;
    
    void Start()
    {
        StartCountingClock();
    }
    private void Update()
    {
        //PauseGame();
    }
    private void StartCountingClock()
    {
        ClockTimer
                    .SetDuration(NumberTime)
                    .OnBegin(() =>Debug.Log("Timer started"))
                    .OnChange((remainingSeconds) => Debug.Log("Timer changed : " + remainingSeconds))
                    .OnEnd(() => Debug.Log("Timer ended"))
                    //.OnPause(IsPaused => Debug.Log("----Paused : "+ IsPaused))
                    .Begin();
    }
    private void PauseGame()
    {
        if (Input.GetKeyDown("p"))
        {
            ClockTimer.SetPause(!ClockTimer.IsPause);
        }
    }
}
