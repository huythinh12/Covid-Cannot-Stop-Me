using System;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class VirusAnimationsManager : MonoBehaviour
{
    private Animator anim;
    private int layerIndexDeath;
    private int layerGetHit;
    private int isWalking;
    private int death;
    private int isRunning;

    private int isIdle;

    private int getHit;
    private VirusDetectCollider virusDetectCollider;
    private VirusHealth virusHealth;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        virusDetectCollider = GetComponent<VirusDetectCollider>();
        virusHealth = GetComponent<VirusHealth>();
        
        anim = GetComponent<Animator>();
        layerIndexDeath = anim.GetLayerIndex("Death");
        layerGetHit = anim.GetLayerIndex("GetHit");
        isWalking = Animator.StringToHash("isWalking");
        isRunning = Animator.StringToHash("isRunning");
        isIdle = Animator.StringToHash("isIdle");
        getHit = Animator.StringToHash("getHit");
        death = Animator.StringToHash("death");
        
        agent = GetComponent<NavMeshAgent>();

        //set layer weight
        //anim.SetLayerWeight(layerIndexDeath,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VirusHealth>().Health > 0)
            isAgentMove();
        else
        {
            anim.SetLayerWeight(layerIndexDeath,1);
        }
    }

    public void OnEnable()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.RegisterEvent<bool>("Chasing", ReceivedEvent);
        virusDetectCollider.UnityEvent_OnGetHit.AddListener(ReceivedEventGetHit);
        virusHealth.UnityEvent_OnDeath.AddListener(ReceivedEventDeath);
    }

    public void OnDisable()
    {
        var behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.UnregisterEvent<bool>("Chasing", ReceivedEvent);
        virusDetectCollider.UnityEvent_OnGetHit.RemoveListener(ReceivedEventGetHit);
        virusHealth.UnityEvent_OnDeath.RemoveListener(ReceivedEventDeath);

    }

    private void ReceivedEventDeath()
    {
        anim.SetTrigger(death);
    }
    
    public void ReceivedEventGetHit()
    {
        anim.SetLayerWeight(layerGetHit,1);
        anim.SetTrigger(getHit);
        Invoke("ResetGetHit",1f);
    }

    private void ResetGetHit()
    {
        anim.SetLayerWeight(layerGetHit,0);
    }
    public void ReceivedEvent(bool isChasing)
    {
        if (isAgentMove())
            if (isChasing)
            {
                anim.SetBool(isRunning, true);
                anim.SetBool(isWalking, false);
            }
            else
            {
                anim.SetBool(isRunning, false);
                anim.SetBool(isWalking, true);
            }
    }

    private bool isAgentMove()
    {
        if (agent.velocity.magnitude >= 0.1f)
        {
            anim.SetBool(isIdle, false);
            return true;
        }

        anim.SetBool(isIdle, true);
        anim.SetBool(isRunning, false);
        anim.SetBool(isWalking, false);
        return false;
    }
}