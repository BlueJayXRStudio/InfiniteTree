using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Unconscious : ActionBehavior
    {
        // bool isPickedUp = false;
        public Unconscious(GameObject go) : base(go) { }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            if (true) {
                Debug.Log("Getting Transported");
                var nextState = DriverObject.GetComponent<CivilianBehaviorFactory>().GetState(typeof(InTransport), DriverObject);
                memory.Push((InTransport) nextState);
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
