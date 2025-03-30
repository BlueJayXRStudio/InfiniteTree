using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GoToStoreBehavior : Behavior
    {        
        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (EarlyTerminator.ShouldTerminate(memory) != Status.RUNNING) {
                memory.Push(this);
                return EarlyTerminator.ShouldTerminate(memory);
            }

            memory.Push(this);

            if (go.GetComponent<Attributes>().GetPos != ExperimentBlackboard.Instance.GroceryStorePos) {
                Debug.Log("Going to the grocery store!");

                memory.Push(
                    go.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(go, 
                        ExperimentBlackboard.Instance.GroceryStorePos)
                );

                return Status.RUNNING;
            }
            else {
                go.GetComponent<Attributes>().FoodItem += 1;
                go.GetComponent<Attributes>().Cash -= 15;
                return Status.SUCCESS;
            }
        }
    }
}