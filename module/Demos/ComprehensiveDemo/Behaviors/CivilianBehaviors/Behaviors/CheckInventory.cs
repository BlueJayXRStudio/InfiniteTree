using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CheckInventory : Behavior
    {
        public CheckInventory(GameObject go) : base(go) {}

        public override Status CheckRequirement()
        {
            if (DriverObject.GetComponent<Attributes>().FoodItem == 0)
                return Status.FAILURE;
            return Status.SUCCESS;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            return CheckRequirement();
        }
    }
}