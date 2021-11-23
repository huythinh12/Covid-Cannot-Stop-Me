using UnityEngine;
using UnityEngine.AI;
public class ZoneMovementController : MonoBehaviour
{
    public static ZoneMovementController Instance;
    public Transform pointRandom;
    public float Range;

    private void Awake()
    {
        Instance = this;
   
    }

    bool RandomPoint (Vector3 center, float range, out Vector3 result)
    {
            Vector3 randomPoint =  center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition (randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            result = Vector3.zero;

        return false;
    }
    public Vector3 GetRandomPoint ()
    {
        Vector3 _point = Vector3.zero;

        if (RandomPoint (transform.position,  Range , out _point))
        {
            Debug.DrawRay (_point, Vector3.up, Color.red, 5);
            pointRandom.position = _point;
            return _point;
        }

        return _point;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, Range);
    }

#endif
}