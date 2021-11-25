using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationsController : MonoBehaviour
{
    private Animator anim;
    private int isWalk;
    private int isCough;
    private int death;
    private NavMeshAgent agent;

    private float saveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isWalk = Animator.StringToHash("isWalk");
        isCough = Animator.StringToHash("isCough");
        death = Animator.StringToHash("death");
        agent = GetComponent<NavMeshAgent>();
        saveSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        var state = GetComponent<NPCState>();
        if (state)
        {
            if (state.currentHealth <= 0)
            {
                anim.SetTrigger(death);
                return;
            }

            if (state.isVirusInside)
            {
                anim.SetBool(isCough, true);
                agent.speed = 2;
            }
            else
            {
                anim.SetBool(isCough, false);
                agent.speed = saveSpeed;
            }
        }


        if (IsAgentMove())
        {
            anim.SetBool(isWalk, true);
        }
        else
        {
            anim.SetBool(isWalk, false);
        }
    }

    private bool IsAgentMove()
    {
        //check if agent not move
        if (agent.isActiveAndEnabled)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }


        return true;
    }
}