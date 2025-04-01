using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Unconscious : Behavior
    {
        public Unconscious(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        // bool isPickedUp = false;        
        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (!go.GetComponent<CivilianAttributes>().ForceWake) {
                memory.Push(this);
                return Status.RUNNING;
            }

            var nextState = go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Recover), go);
            memory.Push((Recover) nextState);
            return Status.RUNNING;
        }
    }
}
