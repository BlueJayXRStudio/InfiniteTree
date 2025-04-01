using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class a_Purchase : Behavior
    {
        public a_Purchase(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            var result = TreeRequirement(memory);
            if (result != Status.RUNNING) {
                memory.Push(this);
                return result;
            }

            memory.Push(this);

            go.GetComponent<Attributes>().FoodItem += 1;
            go.GetComponent<Attributes>().Cash -= 15;
            return Status.SUCCESS;
        }
    }
}