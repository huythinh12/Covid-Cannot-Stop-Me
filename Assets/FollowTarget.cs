using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class FollowTarget : MonoBehaviour
{
    public Transform sp1,sp2;
    
    private NavMeshAgent agent;
    private float offsetDistance = 3f;
    private PlayerHealth player;

    private int randomStanBy;
    // Start is called before the first frame update
    void Start()
    {
        randomStanBy = Random.Range(-1, 3);
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.hasPath)
        {
            if (transform.name.EndsWith("A"))
            {
                agent.SetDestination(sp1.position);
                transform.LookAt(sp1.position);
            }
            else
            {
                transform.LookAt(sp2.position);
                agent.SetDestination(sp2.position);
            }
        }

        if (agent.hasPath && agent.remainingDistance <= 0.1f)
        {
            Invoke("RotateDelay",1f);
        }
    }

    private void RotateDelay()
    {
        transform.DORotate(player.transform.eulerAngles, 2);
        
    }
}
