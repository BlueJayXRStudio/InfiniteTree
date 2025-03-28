using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class InTransport : Behavior
    {
        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (true) {
                Debug.Log("Starting Recovery");
                var nextState = go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Recover));
                memory.Push((Recover) nextState);
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
