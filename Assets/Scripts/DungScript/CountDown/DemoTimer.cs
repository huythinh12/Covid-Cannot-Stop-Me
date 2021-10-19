using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTimer : MonoBehaviour
{
    [SerializeField] private Timer ClockTimer;
    [SerializeField] private int NumberTime;

    // Start is called before the first frame update
    void Start()
    {
        ClockTimer
            .SetDuration(NumberTime)
            .OnBegin(() =>Debug.Log("Timer started"))
            .OnChange((remainingSeconds) => Debug.Log("Timer changed : " + remainingSeconds))
            .OnEnd(() => Debug.Log("Timer ended"))
            .OnPause(IsPaused => Debug.Log("----Paused : "+ IsPaused))
            .Begin();
    }

    private void Update()
    {
        if(Input.GetKeyUp("p"))
        {
            ClockTimer.SetPause(!ClockTimer.IsPause);
        }
    }
}
