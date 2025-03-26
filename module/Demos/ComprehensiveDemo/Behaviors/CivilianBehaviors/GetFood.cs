using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetFood : ActionBehavior
    {
        public GetFood(GameObject go) : base(go) {}

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            memory.Push(new Sequence(new List<Behavior>() {
                new GetCashBehavior(DriverObject),
                new GoToStoreBehavior(DriverObject)
            }));
            return Status.RUNNING;
        }
    }
}