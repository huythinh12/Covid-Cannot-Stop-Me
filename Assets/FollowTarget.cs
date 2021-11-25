using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private float offsetDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // sp1 = GameObject.FindGameObjectWithTag("FollowPointA").transform;
        // sp2 = GameObject.FindGameObjectWithTag("FollowPointB").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(FindObjectOfType<PlayerMovementController>().transform.position);
        }

        if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(transform.position);
            Invoke("RotateDelay",1f);
        }
    }

    private void RotateDelay()
    {
        transform.DORotate(FindObjectOfType<PlayerMovementController>().transform.eulerAngles, 1);
    }
}
