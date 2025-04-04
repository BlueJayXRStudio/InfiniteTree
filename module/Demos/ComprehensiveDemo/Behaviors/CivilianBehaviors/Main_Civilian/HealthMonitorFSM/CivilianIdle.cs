using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianIdle : Behavior
    {
        public CivilianIdle(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            Behavior nextState = this;

            if (go.GetComponent<Attributes>().Health < 20) {
                Debug.Log("Civilian passed out");    
                // Call EMS
                ExperimentBlackboard.Instance.SetCall(go);
                // Pause main control flow tree. An unconscious person most likely would not be thinking :O
                go.GetComponent<CivilianDriver>().SwitchTree();
                // Set next state as "Unconscious"
                nextState = (Unconscious) go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Unconscious), go);
            }
            // By only pushing the next state, we ensure that the next state will be on top of the stack memory.
            memory.Push(nextState);
            return Status.RUNNING;
        }
    }
}


