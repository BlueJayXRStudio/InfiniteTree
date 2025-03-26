using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetCashBehavior : Behavior
    {
        private GameObject DriverObject;
        private float BaseCost = 1.0f;

        public GetCashBehavior(GameObject go) {
            DriverObject = go;
        }

        public Status Step(Stack<Behavior> memory, Status message)
        {
            memory.Push(this);

            // Normally this would require a selector, but we can flexibly exit out
            // of a behavior with basic conditional checks.
            if (DriverObject.GetComponent<Attributes>().Cash >= 25) {
                Debug.Log("We already have enough money");
                return Status.SUCCESS;
            }

            // if not at the ATM already
            if (DriverObject.GetComponent<Attributes>().GetPos != ExperimentBlackboard.Instance.ATMPos) {
                Debug.Log("Going to the nearest ATM");

                memory.Push(
                    DriverObject.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(DriverObject, 
                        ExperimentBlackboard.Instance.ATMPos)
                );
                return Status.RUNNING;
            }
            else {
                Debug.Log("Retrieved cash");
                DriverObject.GetComponent<Attributes>().Cash += 50;
                return Status.SUCCESS;
            }
        }

    }
}