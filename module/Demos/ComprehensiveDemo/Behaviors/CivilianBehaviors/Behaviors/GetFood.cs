using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetFood : Behavior
    {
        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(new Sequence(new List<Behavior>() {
                new GetCashBehavior(),
                new GoToStoreBehavior()
            }));
            return Status.RUNNING;
        }
    }
}