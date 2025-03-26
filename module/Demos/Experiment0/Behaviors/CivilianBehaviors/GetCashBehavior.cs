using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetCashBehavior : Behavior
    {
        private GameObject DriverObject;

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
                    new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(
                            ExperimentBlackboard.Instance.map, 
                            DriverObject.GetComponent<Attributes>().GetPos, 
                            ExperimentBlackboard.Instance.ATMPos
                        ), 
                        DriverObject
                    )
                );
                return Status.RUNNING;
            }
            else {
                Debug.Log("Retrieved cash. Falling back to previous behavior");
                DriverObject.GetComponent<Attributes>().Cash += 50;
                return Status.SUCCESS;
            }
        }
    }
}