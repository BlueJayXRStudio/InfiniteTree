using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public abstract class ActionBehavior : Behavior
    {
        protected GameObject DriverObject;
        public ActionBehavior(GameObject go) => DriverObject = go;

        public abstract Status Step(Stack<Behavior> memory, Status message);
        
    }
}
