using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianIdle : ActionBehavior
    {
        public CivilianIdle(GameObject go) : base(go) { }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            return Status.RUNNING;
        }
    }
}
