using System;
using Player;
using UnityEngine;
using UnityEngine.AI;


public class NPCAnimationsController : MonoBehaviour
{
    private Animator anim;
    private int isWalk;
    private int isRun;
    private int isCough;
    private int death;
    private int isHealing;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isRun = Animator.StringToHash("isRun");
        isWalk = Animator.StringToHash("isWalk");
        isCough = Animator.StringToHash("isCough");
        death = Animator.StringToHash("death");
        isHealing = Animator.StringToHash("isHealing");
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        SupportTarget.OnHealing?.AddListener(ActiveHealing);
    }

    private void ActiveHealing()
    {
        anim.SetBool(isHealing, true);
        Invoke("ResetAnim", 2.5f);
    }

    private void ResetAnim()
    {
        anim.SetBool(isHealing, false);
    }

    private void OnDisable()
    {
        SupportTarget.OnHealing?.RemoveListener(ActiveHealing);
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
            }
            else
            {
                anim.SetBool(isCough, false);
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
        else
        {
            //animation for ally
            if (transform.CompareTag("Ally"))
            {
                if (IsAgentMove())
                {
                    if (PlayerMovementController.isRunning)
                    {
                        anim.SetBool(isRun, true);
                        anim.SetBool(isWalk, false);
                    }
                    else if (PlayerMovementController.isMove)
                    {
                        anim.SetBool(isRun, false);
                        anim.SetBool(isWalk, true);
                    }
                }
                else
                {
                    anim.SetBool(isRun, false);
                    anim.SetBool(isWalk, false);
                }
            }
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