using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.Events;

[TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}CanSeeObjectIcon.png")]
public class ViewInSight : Conditional
{
    [UnityEngine.Tooltip("The field of view angle of the agent (in degrees)")]
    public SharedFloat fieldOfViewAngle;
    [UnityEngine.Tooltip("The distance that the agent can see")]
    public SharedFloat viewDistance;
    public SharedTransform target;
    public SharedColor colorLight;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();
    
    private Transform[] possibleTargets;
    private int isRunning;
    
    public override TaskStatus OnUpdate()
    {
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
       // visibleTargets.Clear();
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
                    //tạm thời chưa dùng tới
                    //visibleTargets.Add (target); đây là target nằm trong tầm nhắm view của a.i để truyền giá trị này đi cho nơi khác dùng 
                    return true;
                }
            
            }
        }
        return false;
    }
}