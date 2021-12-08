using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NPCAnimationsController))]


public class NPCPatrolRandom : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Transform> movePoints = new List<Transform>();
    private int indexPoint;
    private string nameOfZone;

    private void Awake()
    {
        // movePoints = GameObject.FindGameObjectsWithTag(tagNameToMove).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetListMovePointToZone();
        agent = GetComponent<NavMeshAgent>();
    }

    private void SetListMovePointToZone()
    {
        foreach (Transform objPoint in transform.parent?.parent)
        {
            if (objPoint.CompareTag("Move Point"))
            {
                movePoints.Add(objPoint);
            }
        }
    }

    private void Update()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(SetRandomIndexPoint());
        }

        if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(transform.position);
        }
    }

    private Vector3 SetRandomIndexPoint()
    {
        indexPoint = Random.Range(0, movePoints.Count);
        var posToMove = movePoints[indexPoint].transform.position + Vector3.up * 0;
        return posToMove;
    }
}