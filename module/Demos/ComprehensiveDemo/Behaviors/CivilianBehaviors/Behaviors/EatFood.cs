using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatFood : Behavior
    {
        bool Finished = false;
        bool AteFood = false;

        public EatFood(GameObject go) : base(go) { }

        public override Status CheckRequirement()
        {
            if (Finished && AteFood)
                return Status.SUCCESS;
            if (Finished && !AteFood)
                return Status.FAILURE;
            return Status.RUNNING;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            var result = TreeRequirement(memory);
            if (result != Status.RUNNING) {
                Finished = true;
                return result;
            }

            go.GetComponent<Attributes>().FoodItem -= 1;
            go.GetComponent<Attributes>().Health += 15;

            Finished = true;
            AteFood = true;
            return Status.SUCCESS;
        }
    }
}