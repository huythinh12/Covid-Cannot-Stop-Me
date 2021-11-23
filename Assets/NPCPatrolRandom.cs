using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPCPatrolRandom : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] movePoints;
    private int indexPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        movePoints = GameObject.FindGameObjectsWithTag("Move Point");
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

    IEnumerator RandomMove()
    {
        agent.SetDestination(SetRandomIndexPoint());

        yield return null;
    }

    private Vector3 SetRandomIndexPoint()
    {
        indexPoint = Random.Range(0,movePoints.Length-1);
        return movePoints[indexPoint].transform.position;
    }
}