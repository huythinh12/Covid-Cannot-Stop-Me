using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Tutorials
{
    [TaskCategory("Tutorial")]
    [TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}SeekIcon.png")]
    public class FollowRandomPoint : Action
    {
        private NavMeshAgent agent;
        private Vector3 sourcePoint;


        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {

            if (!agent.hasPath && agent.isActiveAndEnabled)
            {
                sourcePoint = ZoneMovementController.Instance.GetRandomPoint(transform);
                agent.SetDestination(sourcePoint);
                transform.DOLookAt(sourcePoint, 2);
            }

            return TaskStatus.Running;
        }
    }
}