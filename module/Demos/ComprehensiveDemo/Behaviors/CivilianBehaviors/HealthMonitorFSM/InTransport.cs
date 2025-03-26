using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class InTransport : ActionBehavior
    {
        public InTransport(GameObject go) : base(go)
        {
        }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            if (true) {
                Debug.Log("Starting Recovery");
                var nextState = DriverObject.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Recover), DriverObject);
                memory.Push((Recover) nextState);
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
