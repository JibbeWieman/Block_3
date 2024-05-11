using System;

namespace BehaviorTree
{
    public class Conditional : Node
    {
        private Func<bool> condition;

        public Conditional(Func<bool> condition)
        {
            this.condition = condition;
        }

        public override NodeState Evaluate()
        {
            if (condition())
            {
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}