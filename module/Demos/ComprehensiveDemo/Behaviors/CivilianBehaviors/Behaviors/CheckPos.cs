using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class CheckPos : Behavior
    {
        private (int, int) pos;

        public CheckPos(GameObject go, (int, int) pos) : base(go)
        {
            Debug.Log($"Checking if we're at {pos}");
            this.pos = pos;
        }

        public override Status CheckRequirement()
        {
            var currPos = DriverObject.GetComponent<Attributes>().transform.position;
            var diff = new Vector2(currPos.x, currPos.z) - new Vector2(pos.Item1, pos.Item2);
            if (DriverObject.GetComponent<Attributes>().GetPos == pos && diff.magnitude < 0.1f)
                return Status.SUCCESS;
            return Status.FAILURE;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);
            return CheckRequirement();
        }
    }
}