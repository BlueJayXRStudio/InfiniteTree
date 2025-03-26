using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatBehavior : Behavior
    {

        private GameObject DriverObject;

        public EatBehavior(GameObject go) {
            DriverObject = go;
        }

        public Status Step(Stack<Behavior> memory, Status message)
        {
            memory.Push(this);

            if (DriverObject.GetComponent<Attributes>().Cash < 25) {
                Debug.Log("Not enough cash. Going to the ATM");
                memory.Push(new GetCashBehavior(DriverObject));
                return Status.RUNNING;
            }
            else {
                Debug.Log("Got enough cash, but behavior is not yet implemented :O");
            }

            return Status.RUNNING;
        }
    }
}
