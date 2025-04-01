using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class GoToStore : Behavior
    {
        public GoToStore(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            throw new System.NotImplementedException();
        }
    }
}