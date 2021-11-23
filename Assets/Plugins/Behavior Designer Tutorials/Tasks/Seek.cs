using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Tutorials
{
    [TaskCategory("Tutorial")]
    [TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}SeekIcon.png")]
    public class Seek : Action
    {
        [Tooltip("The GameObject that the agent is seeking")]
        public SharedTransform target;

        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;

        public SharedBool isTimeChasingOut;
        public SharedTransform pointToPatrol;
        public SharedTransform pointToMission;

        // Component references
        protected UnityEngine.AI.NavMeshAgent navMeshAgent;

        private BehaviorTree behaviorTree;

        //"The agent has arrived when the destination is less than the specified amount. This distance should be greater than or equal to the NavMeshAgent StoppingDistance."
        private float arriveDistance = 0.2f;
        private bool isColliderHuman;

        /// <summary>
        /// Cache the component references.
        /// </summary>
        public override void OnAwake()
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            behaviorTree = GetComponent<BehaviorTree>();
        }

        /// <summary>
        /// Allow pathfinding to resume.
        /// </summary>
        public override void OnStart()
        {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
            navMeshAgent.isStopped = false;
#endif

            SetDestination(Target());
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            SetDestination(Target());

            return TaskStatus.Running;
        }

        // Return targetPosition if target is null
        private Vector3 Target()
        {
            if (target.Value != null)
            {
                behaviorTree.SendEvent<bool>("Chasing", true);
                transform.LookAt(target.Value.position);
                return target.Value.position;
            }

            behaviorTree.SendEvent<bool>("Chasing", false);

            if (pointToMission.Value != null)
            {
                return pointToMission.Value.position;
            }

            return pointToPatrol.Value != null ? pointToPatrol.Value.position : targetPosition.Value;
        }

        /// <summary>
        /// Set a new pathfinding destination.
        /// </summary>
        /// <param name="destination">The destination to set.</param>
        /// <returns>True if the destination is valid.</returns>
        private void SetDestination(Vector3 destination)
        {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
            navMeshAgent.Resume();
#else
            navMeshAgent.isStopped = false;
#endif
            navMeshAgent.SetDestination(destination);
        }

        /// <summary>
        /// Has the agent arrived at the destination?
        /// </summary>
        /// <returns>True if the agent has arrived at the destination.</returns>
        private bool HasArrived()
        {
            // The path hasn't been computed yet if the path is pending.
            float remainingDistance;
            if (navMeshAgent.pathPending)
            {
                remainingDistance = float.PositiveInfinity;
            }
            else
            {
                remainingDistance = navMeshAgent.remainingDistance;
            }

            if (isColliderHuman && behaviorTree)
            {
                behaviorTree.SendEvent<Transform>("CatchedPlayer",target.Value);
                return false;
            }
            return remainingDistance <= arriveDistance;
        }

        /// <summary>
        /// Stop pathfinding.
        /// </summary>
        private void Stop()
        {
            if (navMeshAgent.hasPath)
            {
#if UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
                navMeshAgent.Stop();
#else
                navMeshAgent.isStopped = true;
                isTimeChasingOut.Value = true;
#endif
            }
        }

        /// <summary>
        /// The task has ended. Stop moving.
        /// </summary>
        public override void OnEnd()
        {
            Stop();
        }

        /// <summary>
        /// The behavior tree has ended. Stop moving.
        /// </summary>
        public override void OnBehaviorComplete()
        {
            Stop();
        }

        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.collider.CompareTag("Player"))
            {
                isColliderHuman = true;
            }
        }

      
    }
}