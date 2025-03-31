using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatBehavior : Behavior
    {

        public EatBehavior(GameObject go) : base(go) => DriverObject = go;

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (CheckTermination() == Status.SUCCESS) {
                return Status.SUCCESS;
            }

            if (go.GetComponent<Attributes>().FoodItem == 0) {
                Debug.Log("Checking for food");
                memory.Push(new GetFood(DriverObject));
                return Status.RUNNING;
            }
            else {
                go.GetComponent<Attributes>().FoodItem -= 1;
                go.GetComponent<Attributes>().Health += 20;
                return Status.SUCCESS;
            }
            // return Status.FAIL;
        }


        // We are doing this because health is less than certain amount (i.e. HP < 80)
        public override Status CheckSuccess()
        {
            if (DriverObject.GetComponent<Attributes>().Health >= 80)
                return Status.SUCCESS;

            return Status.RUNNING;
        }

        // We will keep trying until success. So trivially always running otherwise.
        public override Status CheckFailure()
        {
            return Status.RUNNING;
        }


    }
}
