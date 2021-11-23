using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Tutorials
{
    [TaskCategory("Tutorial")]
    [TaskIcon("Assets/Behavior Designer Tutorials/Tasks/Editor/{SkinColor}SeekIcon.png")]
    public class ResetAgent : Action
    {
        public SharedTransform target;
        private NavMeshAgent agent;
        public override void OnAwake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (agent)
            {
                agent.ResetPath();
                target.Value = null;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}


