using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SupportTarget : MonoBehaviour
{
    public static UnityEvent<ParticleSystem> OnHealing = new UnityEvent<ParticleSystem>();

    private void OnEnable()
    {
        PlayerHealth.OnGetHitHeal?.AddListener(PlayerGetHit);
    }

    private void OnDisable()
    {
        PlayerHealth.OnGetHitHeal?.RemoveListener(PlayerGetHit);
    }

    private void PlayerGetHit(int health, ParticleSystem healEffect)
    {
        StartCoroutine(BuffDelay(health, healEffect));
    }

    IEnumerator BuffDelay(int health, ParticleSystem healEffect)
    {
        yield return new WaitForSeconds(2);

        if (health > 0)
        {
            OnHealing?.Invoke(healEffect);
        }
    }
}