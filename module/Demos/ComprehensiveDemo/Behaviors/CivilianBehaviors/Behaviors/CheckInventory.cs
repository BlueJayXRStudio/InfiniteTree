using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CheckInventory : Behavior
    {
        public CheckInventory(GameObject go) : base(go) {}

        public override Status CheckFailure() {
            if (DriverObject.GetComponent<Attributes>().FoodItem == 0)
                return Status.FAILURE;
            return Status.RUNNING;
        }

        public override Status CheckSuccess() {
            if (DriverObject.GetComponent<Attributes>().FoodItem > 0)
                return Status.SUCCESS;
            // CheckSuccess is an atomic action, but in an abstract sense,
            // it is still an action suspended across a temporal dimension.
            // Thus, even though we can exit the program CheckSuccess() if
            // we happen to have enough FoodItem, we cannot determine if
            // we will eventually have enough FoodItem or not. Therefore,
            // we can declare that the procedure is running. Similar
            // reasoning applies to CheckFailure().
            return Status.RUNNING;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (CheckSuccess() == Status.SUCCESS) 
                return Status.SUCCESS;
            
            if (CheckFailure() == Status.FAILURE)
                return Status.FAILURE;
                
            return Status.RUNNING;
        }
    }
}