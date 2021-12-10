using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class CrossFade : MonoBehaviour
{
    private void OnEnable()
    {
        SceneLoadingManager.OnEventFadeIn.AddListener(ReceivedEventCrossFade);
    }

    private void OnDisable()
    {
        SceneLoadingManager.OnEventFadeIn.RemoveListener(ReceivedEventCrossFade);
    }

    private void ReceivedEventCrossFade(bool isFadeIn)
    {
        if (isFadeIn)
        {
            GetComponent<Image>().raycastTarget = false;
            StartCoroutine(FadeIn());
        }
        else
        {
            GetComponent<Image>().raycastTarget = true;
            FadeOut();
        }
    }

    IEnumerator FadeIn()
    {
        GetComponent<Image>().DOFade(0, SceneLoadingManager.Instance.timeToCrossFade).SetEase(Ease.Linear)
            .SetUpdate(true);
        yield return new WaitForSeconds(1);
        SceneLoadingManager.hasLoadingDone = true;
    }

    public void FadeOut()
    {
        GetComponent<Image>().DOFade(1, SceneLoadingManager.Instance.timeToCrossFade).SetEase(Ease.Linear)
            .SetUpdate(true);
    }
}