using UnityEngine;
using UnityEngine.AI;


public class NPCActionController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 sourcePoint;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (!agent.hasPath && agent.isActiveAndEnabled)
        {
            sourcePoint = ZoneMovementController.Instance.GetRandomPoint(transform);
            agent.SetDestination(sourcePoint);
            transform.LookAt(sourcePoint);
        
        }
    }

  
}