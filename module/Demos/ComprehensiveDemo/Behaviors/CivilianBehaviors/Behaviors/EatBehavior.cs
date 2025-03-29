using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatBehavior : Behavior, ICheckTermination
    {
        public GameObject DriverObject;

        public EatBehavior(GameObject go) => DriverObject = go;

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (go.GetComponent<Attributes>().FoodItem == 0) {
                Debug.Log("Checking for food");
                memory.Push(new GetFood());
                return Status.RUNNING;
            }
            else {
                go.GetComponent<Attributes>().FoodItem -= 1;
                go.GetComponent<Attributes>().Health += 20;
                return Status.SUCCESS;
            }
            // return Status.FAIL;
        }

        public Status CheckTermination() {
            if (DriverObject.GetComponent<Attributes>().FoodItem > 0)
                return Status.SUCCESS;
            else
                return Status.RUNNING;
        }

    }
}
