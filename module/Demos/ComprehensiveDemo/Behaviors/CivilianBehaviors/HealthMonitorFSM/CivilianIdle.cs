using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianIdle : ActionBehavior
    {
        public CivilianIdle(GameObject go) : base(go) { }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            if (DriverObject.GetComponent<Attributes>().Health < 20) {
                var nextState = DriverObject.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Unconscious));
                // by only pushing the next state, we ensure that the next state will be on top of the stack memory.
                memory.Push((Unconscious) nextState);
            }

            return Status.RUNNING;
        }
    }
}
