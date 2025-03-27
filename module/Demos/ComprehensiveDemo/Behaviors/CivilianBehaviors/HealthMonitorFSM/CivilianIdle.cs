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
                Debug.Log("Civilian passed out");
                var nextState = DriverObject.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Unconscious), DriverObject);
                
                // by only pushing the next state, we ensure that the next state will be on top of the stack memory.
                memory.Push((Unconscious) nextState);
                
                // Implement: EMS.Call() maybe through blackboard
                // Pause main control flow tree. An unconscious person most likely would not be thinking :O
                DriverObject.GetComponent<CivilianDriver>().SwitchTree();
                
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}


