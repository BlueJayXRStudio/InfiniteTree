using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetFood : Behavior, ICheckTermination
    {
        private GameObject DriverObject;

        public GetFood(GameObject go) {
            DriverObject = go;
        }

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (message == Status.SUCCESS)
                return Status.SUCCESS;
                   
            memory.Push(new Sequence(new List<Behavior>() {
                new GetCashBehavior(go),
                new GoToStoreBehavior(go)
            }));
            return Status.RUNNING;
        }

        public Status CheckTermination() {
            if (DriverObject.GetComponent<Attributes>().FoodItem > 0)
                return Status.SUCCESS;
            else
                return Status.RUNNING;
        }
    }
}