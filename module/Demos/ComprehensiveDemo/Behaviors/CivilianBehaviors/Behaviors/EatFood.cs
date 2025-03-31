using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatFood : Behavior
    {
        public EatFood(GameObject go) : base(go)
        {
        }

        public override Status CheckFailure()
        {
            throw new System.NotImplementedException();
        }

        public override Status CheckSuccess()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            throw new System.NotImplementedException();
        }
    }
}