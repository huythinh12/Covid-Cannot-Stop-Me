using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.Events;

[TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}CanSeeObjectIcon.png")]
public class WithinSight : Conditional
{
    public string targetTag;

    [UnityEngine.Tooltip("The field of view angle of the agent (in degrees)")]
    public SharedFloat fieldOfViewAngle ;

    [UnityEngine.Tooltip("The distance that the agent can see")]
    public SharedFloat viewDistance ;
    public SharedTransform target;
    public Animator anim;
    
    private Transform[] possibleTargets;
    private int isRunning;
    
    
    public override void OnAwake()
    {
        base.OnAwake();
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        possibleTargets = new Transform[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            possibleTargets[i] = targets[i].transform;
        }
        
        anim = GetComponent<Animator>();
        isRunning = Animator.StringToHash("isRunning");
        
    }

    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (withinSight(possibleTargets[i], fieldOfViewAngle))
            {
                target.Value = possibleTargets[i];
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }

    public bool withinSight(Transform targetTransform, SharedFloat fieldOfViewAngle)
    {
        Vector3 direction = targetTransform.position - transform.position;
        float dist = Vector3.Distance(transform.position, targetTransform.position);
        return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle.Value && dist <= viewDistance.Value;
    }
    
}