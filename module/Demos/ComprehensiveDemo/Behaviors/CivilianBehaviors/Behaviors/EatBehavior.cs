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

            if (DriverObject.GetComponent<Attributes>().FoodItem == 0) {
                Debug.Log("Checking for food");
                memory.Push(new GetFood(DriverObject));
                return Status.RUNNING;
            }
            else {
                DriverObject.GetComponent<Attributes>().FoodItem -= 1;
                DriverObject.GetComponent<Attributes>().Health += 20;
                return Status.SUCCESS;
            }
            // return Status.FAIL;
        }
    }
}
