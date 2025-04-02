using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EMSControlFlow : Behavior
    {
        public EMSControlFlow(GameObject go) : base(go) { }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);
            GameObject Call = ExperimentBlackboard.Instance.GetCall;
            
            if (Call != null)
                memory.Push(new TransportPatient(go, Call));

            return Status.RUNNING;
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }
    }
}
