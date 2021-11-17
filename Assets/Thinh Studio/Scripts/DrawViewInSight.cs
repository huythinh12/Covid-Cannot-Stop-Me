using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class DrawViewInSight : Action
{
    [UnityEngine.Tooltip("The field of view angle of the agent (in degrees)")]
    public SharedFloat fieldOfViewAngle = 90;

    [UnityEngine.Tooltip("The distance that the agent can see")]
    public SharedFloat viewDistance;

    // public LayerMask obstacleMask = LayerMask.NameToLayer("Obstacles");
    public SharedBool isTimeChasingOut;
    public SharedColor colorLight;
    private float saveDist;

    public override void OnAwake()
    {
        saveDist = viewDistance.Value;
    }

    /// <summary>
    /// Draws the line of sight representation
    /// </summary>
    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (isTimeChasingOut.Value)
            colorLight.Value = Color.white;
        ViewRange();
        var oldColor = UnityEditor.Handles.color;
        var color = colorLight.Value;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;

        var halfFOV = fieldOfViewAngle.Value * 0.5f;
        var beginDirection = Quaternion.AngleAxis(-halfFOV, Vector3.up) * Owner.transform.forward;
        UnityEditor.Handles.DrawSolidArc(Owner.transform.position, Owner.transform.up, beginDirection,
            fieldOfViewAngle.Value, saveDist);

        UnityEditor.Handles.color = oldColor;
#endif
    }

    private void ViewRange()
    {
        if (viewDistance.Value != null)
        {
            saveDist = viewDistance.Value;
        }

        RaycastHit hit;
        if (transform != null)
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hitInfo: out hit,
                saveDist))
            {
                if (!hit.collider.CompareTag("Target"))
                {
                    saveDist = hit.distance;
                }
            }
    }
}