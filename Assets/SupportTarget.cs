using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SupportTarget : MonoBehaviour
{
    public static UnityEvent OnHealing = new UnityEvent();

    private void OnEnable()
    {
        PlayerHealth.OnGetHit?.AddListener(PlayerGetHit);
    }

    private void OnDisable()
    {
        PlayerHealth.OnGetHit?.RemoveListener(PlayerGetHit);
    }

    private void PlayerGetHit(int def, int health)
    {
        StartCoroutine(BuffDelay(def, health));
    }

    IEnumerator BuffDelay(int def, int health)
    {
        yield return new WaitForSeconds(2);
        
        if (health > 0)
        {
            OnHealing?.Invoke();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}