using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class a_Withdraw : Behavior
    {
        public a_Withdraw(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            var result = TreeRequirement(memory);
            if (result != Status.RUNNING) {
                memory.Push(this);
                return result;
            }

            memory.Push(this);
            
            Debug.Log("Retrieved cash");
            DriverObject.GetComponent<Attributes>().Cash += 50;
            return Status.SUCCESS;
        }
    }
}