using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Unconscious : Behavior
    {
        // bool isPickedUp = false;
        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (true) {
                Debug.Log("Getting Transported");
                var nextState = go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(InTransport), go);
                memory.Push((InTransport) nextState);
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
