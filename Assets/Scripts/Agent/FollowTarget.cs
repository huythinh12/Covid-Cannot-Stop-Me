using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    private NavMeshAgent agent;
    private float offsetDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            if (!agent.hasPath)
            {
                var playerPos = FindObjectOfType<PlayerMovementController>();
                agent.SetDestination(playerPos.transform.position);
            }

            if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(transform.position);
                Invoke("RotateDelay", 1f);
            }
        }
    }

    private void RotateDelay()
    {
        transform.DORotate(FindObjectOfType<PlayerMovementController>().transform.eulerAngles, 1);
    }
}