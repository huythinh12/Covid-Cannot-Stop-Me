using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using BehaviorDesigner.Runtime;

public class NPCActionController : Action
{
    private NavMeshAgent agent;
    public SharedTransform target;


    private void Start ()
    {
        agent = GetComponent<NavMeshAgent> ();
    }

    private void Update ()
    {
       
    }

    public override TaskStatus OnUpdate()
    {
        if (!agent.hasPath && agent.isActiveAndEnabled)
        {
            var point = ZoneMovementController.Instance.GetRandomPoint();
            target.Value.position = point;
            agent.SetDestination (point);
            transform.LookAt(point);
        }

        return TaskStatus.Running;
    }
}