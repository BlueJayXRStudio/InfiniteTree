using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    // Currently this is a single state "FSM"
    public class CivilianControlFlow : Behavior
    {
        Behavior CurrentTask;

        public CivilianControlFlow(GameObject go) : base(go) {}

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
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
                CurrentTask = new EatBehavior(go);
                memory.Push(CurrentTask);
                Debug.Log("Need to Eat Food. Will Try Eating Food");
                return Status.RUNNING;
            }
            
            return Status.RUNNING;
        }

        /// <summary>
        /// We are doing this because we want to keep our health points
        /// above a certain point (i.e. HP >= 80).
        /// </summary>
        public override Status CheckRequirement()
        {
            if (CurrentTask is EatBehavior) {
                if (DriverObject.GetComponent<Attributes>().Health >= 80)
                    return Status.SUCCESS;
                return Status.RUNNING;
            }
            return Status.RUNNING;
        }

        
    }
}
