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
            if (!go.GetComponent<CivilianAttributes>().ForceWake) {
                memory.Push(this);
                return Status.RUNNING;
            }

            var nextState = go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Recover));
            memory.Push((Recover) nextState);
            return Status.RUNNING;
        }
    }
}
