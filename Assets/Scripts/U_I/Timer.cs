using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFillImage;
   
    public int Duration { get; private set; }

    public bool IsPause { get; private set; }

    private int remainingDuration;

    // Delegates
    private UnityAction onTimerBeginAction;
    private UnityAction<int> onTimerChangeAction;
    private UnityAction onTimerEndAction;
    private UnityAction<bool> onTimerPauseAction;

    private void Awake()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        uiFillImage.fillAmount = 0f;
        Duration = remainingDuration = 0;

        onTimerBeginAction = null;
        onTimerChangeAction = null;
        onTimerEndAction = null;
        onTimerPauseAction = null;

        IsPause = false;
    }

    public void SetPause (bool paused)
    {
        IsPause = paused;

        if (onTimerPauseAction != null)
            onTimerPauseAction.Invoke(IsPause);
    }

    public Timer SetDuration (int seconds)
    {
        Duration = remainingDuration = seconds;
        return this;
    }

    //-- Events ----------------------------
    public Timer OnBegin(UnityAction action)
    {
        onTimerBeginAction = action;
        return this;
    }
    public Timer OnChange(UnityAction<int> action)
    {
        onTimerChangeAction = action;
        return this;
    }
    public Timer OnEnd(UnityAction action)
    {
        onTimerEndAction = action;
        return this;
    }
    public Timer OnPause(UnityAction<bool> action)
    {
        onTimerPauseAction = action;
        return this;
    }


    public void Begin()
    {
        if (onTimerBeginAction != null)
            onTimerBeginAction.Invoke();

        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration > 0 && !GameManager.Instance.isEndTime)
        {
            if (!IsPause && !GameManager.Instance.isWin)
            {
                if (onTimerChangeAction != null)
                    onTimerChangeAction.Invoke(remainingDuration);

                UpdateUI(remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        End();
    }

    private void UpdateUI (int seconds)
    {
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Duration, seconds);
    }

    public void End()
    {
        if (onTimerEndAction != null)
            onTimerEndAction.Invoke();
        ResetTimer();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
