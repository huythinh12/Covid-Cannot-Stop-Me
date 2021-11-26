using System;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(300)]
public class VirusDetectCollider : MonoBehaviour
{
    public ParticleSystem particleInject;
    public bool isDestroyUnActiveVirus;
    public UnityEvent UnityEvent_OnGetHit;
    private bool isTurnOn;

    // public void OnEnable()
    // {
    //     var behaviorTree = GetComponent<BehaviorTree>();
    //     behaviorTree.RegisterEvent<Transform>("CatchedPlayer", ReceivedEvent);
    // }
    //
    // public void OnDisable()
    // {
    //     var behaviorTree = GetComponent<BehaviorTree>();
    //     behaviorTree.UnregisterEvent<Transform>("CatchedPlayer", ReceivedEvent);
    // }


    private void Injection(Transform target)
    {
        if (transform != null)
        {
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear);
            transform.DOMove(target.position, 0.3f);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") || other.collider.CompareTag("NPC"))
        {
            //count virus infected
            if (!GameManager.Instance.listInfected.Contains(other.collider.name))
            {
                GameManager.Instance.listInfected.Add(other.collider.name);
            }

            GetComponent<SphereCollider>().isTrigger = true;
            if (!isTurnOn)
            {
                Injection(other.transform);
                isTurnOn = true;
                Invoke("DestroyVirusAfterInject", 0.5f);
            }
        }
        else if (other.collider.CompareTag("Bullet"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
                VirusDefeatNumber.virusNumberDefeat++;
            if (!GetComponent<VirusActiveController>().isActiveVirus)
            {
                var dissolveOver = GetComponent<U10PS_DissolveOverTime>();
                dissolveOver.enabled = true;
                isDestroyUnActiveVirus = true;

                Invoke("DestroyUnActiveVirus", 0.3f);
            }
            else
            {
                UnityEvent_OnGetHit?.Invoke();
                GetComponent<VirusHealth>().GetHit();
            }
        }
    }

    private void DestroyVirusAfterInject()
    {
        Instantiate(particleInject, new Vector3(transform.position.x, 1.2f, transform.position.z),
            Quaternion.identity);
        Destroy(gameObject);
    }

    private void DestroyUnActiveVirus()
    {
        Destroy(gameObject);
    }
}