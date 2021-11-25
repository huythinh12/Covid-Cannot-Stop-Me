using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SupportTargetDef : MonoBehaviour
{
    public static UnityEvent OnBuffDef = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        PlayerHealth.OnGetHit?.AddListener(PlayerGetHit);
    }
    private void OnDisable()
    {
        PlayerHealth.OnGetHit?.RemoveListener(PlayerGetHit);
    }
    private void PlayerGetHit(int def,int health)
    {
        StartCoroutine(BuffDelay(def));
    }

    IEnumerator BuffDelay(int def)
    {
        yield return new WaitForSeconds(3);
        if (def >=0)
        {
            OnBuffDef?.Invoke();
        }
        StopAllCoroutines();

    }
}
