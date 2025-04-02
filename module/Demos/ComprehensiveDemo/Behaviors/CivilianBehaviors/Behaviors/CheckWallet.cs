using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CheckWallet : Behavior
    {
        public CheckWallet(GameObject go) : base(go) => Debug.Log("Checking Wallet");

        public override Status CheckRequirement()
        {
            if (DriverObject.GetComponent<Attributes>().Cash >= 25)
                return Status.SUCCESS;
            return Status.FAILURE;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);
            return CheckRequirement();
        }
    }
}