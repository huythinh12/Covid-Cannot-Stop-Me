using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEditor.Experimental.TerrainAPI;

public class ConditionTrigger : Conditional
{
    public float fieldOFViewAngel;
    public float maxDist;
    public string targetTag;

    public SharedTransform target;

    private Transform[] possibleTargets;
    

    public override void OnAwake()
    {
        base.OnAwake();
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        possibleTargets = new Transform[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            possibleTargets[i] = targets[i].transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (withinSight(possibleTargets[i], fieldOFViewAngel))
            {
                target.Value = possibleTargets[i];
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
    

    public bool withinSight(Transform targetTransform, float fieldOfViewAngle)
    {
        Vector3 direction = targetTransform.position - transform.position;
        float dist = Vector3.Distance(transform.position, targetTransform.position);
        return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle && dist <= maxDist;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
