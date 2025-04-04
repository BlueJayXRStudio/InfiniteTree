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
            Behavior nextState = this;

            if (go.GetComponent<CivilianAttributes>().ForceWake) {
                Debug.Log("Waking up. Starting Recovery :D");
                nextState = (Recover)go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(Recover), go);
            }

            memory.Push(nextState);
            return Status.RUNNING;
        }
    }
}
