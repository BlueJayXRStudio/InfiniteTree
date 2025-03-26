using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianIdle : Behavior
    {
        GameObject DriverObject;
        // We can abstract away Composite vs Action nodes. For action nodes, 
        // we should have DriverObject initialization by default.
        public CivilianIdle(GameObject go) => DriverObject = go;

        public Status Step(Stack<Behavior> memory, Status message)
        {
            // if ()
            return Status.RUNNING;
        }
    }
}
