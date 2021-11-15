using DG.Tweening;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(300)]
public class VirusDetectCollider : MonoBehaviour
{
    public bool isDestroyUnActiveVirus;
    public UnityEvent UnityEvent_OnGetHit;
    public void OnEnable()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.RegisterEvent<Transform>("CatchedPlayer", ReceivedEvent);
    }

    public void OnDisable()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.UnregisterEvent<Transform>("CatchedPlayer", ReceivedEvent);
    }

    public void ReceivedEvent(Transform targetPos)
    {
        GetComponent<SphereCollider>().enabled = false;
        Injection(targetPos);
    }

    private void Injection(Transform target)
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear);
        transform.DOMove(target.position + new Vector3(0, 1.5f, 0), 0.5f);
        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
        }
        else if (other.collider.CompareTag("NPC"))
        {
        }
        else if (other.collider.CompareTag("Bullet"))
        {
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

    private void DestroyUnActiveVirus()
    {
        Destroy(gameObject);
    }
}
