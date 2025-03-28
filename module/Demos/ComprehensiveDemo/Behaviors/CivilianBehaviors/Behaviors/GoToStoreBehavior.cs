using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class GoToStoreBehavior : ActionBehavior
    {
        public GoToStoreBehavior(GameObject go) : base(go) { }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            memory.Push(this);

            if (DriverObject.GetComponent<Attributes>().GetPos != ExperimentBlackboard.Instance.GroceryStorePos) {
                Debug.Log("Going to the grocery store!");

                memory.Push(
                    DriverObject.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(DriverObject, 
                        ExperimentBlackboard.Instance.GroceryStorePos)
                );

                return Status.RUNNING;
            }
            else {
                DriverObject.GetComponent<Attributes>().FoodItem += 1;
                DriverObject.GetComponent<Attributes>().Cash -= 15;
                return Status.SUCCESS;
            }
        }
    }
}