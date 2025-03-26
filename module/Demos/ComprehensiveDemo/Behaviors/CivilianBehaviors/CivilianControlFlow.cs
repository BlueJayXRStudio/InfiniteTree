using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianControlFlow : Behavior
    {
        GameObject DriverObject;
        public CivilianControlFlow(GameObject go) {
            DriverObject = go;
        }
        public Status Step(Stack<Behavior> memory, Status message)
        {
            // Debug.Log("Stepping");
            if (DriverObject.GetComponent<Attributes>().Health < 50) {
                memory.Push(this);
                memory.Push(new EatBehavior(DriverObject));
                Debug.Log("Getting Food");
                return Status.RUNNING;
            }
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
