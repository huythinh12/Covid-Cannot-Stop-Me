using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SupportTargetDef : MonoBehaviour
{
    public static UnityEvent<ParticleSystem> OnBuffDef = new UnityEvent<ParticleSystem>();

    private void OnEnable()
    {
        PlayerHealth.OnGetHitDef?.AddListener(PlayerGetHit);
    }

    private void OnDisable()
    {
        PlayerHealth.OnGetHitDef?.RemoveListener(PlayerGetHit);
    }

    private void PlayerGetHit(int def, ParticleSystem defEffect)
    {
        StartCoroutine(BuffDelay(def, defEffect));
    }

    IEnumerator BuffDelay(int def, ParticleSystem defEffect)
    {
        yield return new WaitForSeconds(3);
        if (def >= 0)
        {
            OnBuffDef?.Invoke(defEffect);
        }

        StopAllCoroutines();
    }
}