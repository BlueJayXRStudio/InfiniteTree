using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    // Currently this is a single state "FSM"
    public class CivilianControlFlow : Behavior
    {
        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            // push this state immediately back in, because we know
            // we will always come back to this state.
            memory.Push(this); 

            if (go.GetComponent<Attributes>().Health < 80) {
                // EatBehavior is a Behavior Tree. Conventionally, it wouldn't
                // be possible to run a Behavior Tree from a state in a state
                // machine, but by treating each behavior as a stackable task
                // we can achieve a general engine capable of switching between
                // an FSM and a Behavior Tree.
                memory.Push(new EatBehavior(go));
                Debug.Log("Getting Food");
                return Status.RUNNING;
            }
            
            return Status.RUNNING;
        }
    }
}
