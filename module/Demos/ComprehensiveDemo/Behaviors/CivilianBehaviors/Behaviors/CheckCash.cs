using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CheckCash : Behavior
    {
        public CheckCash(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            if (DriverObject.GetComponent<Attributes>().Cash >= 25)
                return Status.SUCCESS;
            return Status.FAILURE;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            return CheckRequirement();
        }
    }
}