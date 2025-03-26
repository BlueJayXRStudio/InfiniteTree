using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Unconscious : ActionBehavior
    {
        public Unconscious(GameObject go) : base(go)
        {
        }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            throw new System.NotImplementedException();
        }
    }
}
