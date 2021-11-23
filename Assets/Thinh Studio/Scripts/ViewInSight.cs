using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

[TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}CanSeeObjectIcon.png")]
public class ViewInSight : Conditional
{
    [UnityEngine.Tooltip("The field of view angle of the agent (in degrees)")]
    public SharedFloat fieldOfViewAngle;

    [UnityEngine.Tooltip("The distance that the agent can see")]
    public SharedFloat viewDistance;
    public SharedBool isTargetHere;
    public SharedTransform target;
    public SharedColor colorLight;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

    private Transform[] possibleTargets;
    private int isRunning;
    private float saveViewDistancePersistance;
    private NavMeshAgent agent;

    public override void OnAwake()
    {

        saveViewDistancePersistance = viewDistance.Value;
    }

    public override TaskStatus OnUpdate()
    {
        FieldVisibleTarget();
        if (FindVisibleTargets())
            {
                colorLight.Value = Color.red;
                return TaskStatus.Success;
            }
        

        colorLight.Value = Color.white;
        return TaskStatus.Failure;
    }

    bool FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance.Value, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            target.Value = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.Value.position - transform.position).normalized;
            var myAngle = Vector3.Angle(transform.forward, dirToTarget);
            if (myAngle < fieldOfViewAngle.Value / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.Value.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    isTargetHere.Value = true;
                    return isTargetHere.Value;
                }
                else
                {
                    isTargetHere.Value = false;
                }
            }
        }

        return false;
    }

    private void FieldVisibleTarget()
    {
        viewDistance.Value = saveViewDistancePersistance;
        RaycastHit hit;
        if (transform != null)
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hitInfo: out hit,
                viewDistance.Value))
            {
                if (!hit.collider.CompareTag("Target"))
                {
                    viewDistance.Value = hit.distance;
                }
            }
    }
}