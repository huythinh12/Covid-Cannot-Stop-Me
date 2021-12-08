using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.Collider;
using UnityEngine.Events;
using Unity;
using UnityEngine.PlayerLoop;
using GetEnabled = BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour.GetEnabled;

public class ActionController : Action
{
    public static Transform targetSave;
    public float speed = 0;
    public SharedTransform target;
    public float rangeDetect;
    public Animator anim;
    private int isIdle;
    private int isRunning;

    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
        isIdle = Animator.StringToHash("isIdle");
        isRunning = Animator.StringToHash("isRunning");
    }

    public override void OnStart()
    {
        Debug.Log("xin chao ");
    }


    public override TaskStatus OnUpdate()
    {
        bool isTouchTarget = Vector3.SqrMagnitude(transform.position - target.Value.position) < rangeDetect;
        if (isTouchTarget)
        {
            anim.SetBool(isIdle, true);
            anim.SetBool(isRunning, false);
            return TaskStatus.Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);
            anim.SetBool(isRunning, true);
            transform.LookAt(target.Value);
        }

        return TaskStatus.Running;
    }
}