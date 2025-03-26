using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetCashBehavior : Behavior //, Optimizer
    {
        private GameObject DriverObject;
        private float BaseCost = 1.0f;

        public GetCashBehavior(GameObject go) {
            DriverObject = go;
        }

        public Status Step(Stack<Behavior> memory, Status message)
        {
            memory.Push(this);

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
                Debug.Log("Retrieved cash. Falling back to previous behavior");
                DriverObject.GetComponent<Attributes>().Cash += 50;
                return Status.SUCCESS;
            }
        }

        // We just need a way to form a graph. Then, we can run pathfinding to optimize cost.
        // public List<Behavior> GetNodes() {
        //     return new List<Behavior> { DriverObject.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(DriverObject,
        //                 ExperimentBlackboard.Instance.ATMPos) };
        // }

        // public float GetCost => BaseCost;
    }
}