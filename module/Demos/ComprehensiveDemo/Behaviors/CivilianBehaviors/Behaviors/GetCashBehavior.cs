using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetCashBehavior : Behavior
    {
        private float BaseCost = 1.0f;

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            // Normally this would require a selector, but we can flexibly exit out
            // of a behavior with basic conditional checks.
            if (go.GetComponent<Attributes>().Cash >= 25) {
                Debug.Log("We already have enough money");
                return Status.SUCCESS;
            }

            // if not at the ATM already
            if (go.GetComponent<Attributes>().GetPos != ExperimentBlackboard.Instance.ATMPos) {
                Debug.Log("Going to the nearest ATM");

                memory.Push(
                    go.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(go, 
                        ExperimentBlackboard.Instance.ATMPos)
                );
                return Status.RUNNING;
            }
            else {
                Debug.Log("Retrieved cash");
                go.GetComponent<Attributes>().Cash += 50;
                return Status.SUCCESS;
            }
        }

    }
}