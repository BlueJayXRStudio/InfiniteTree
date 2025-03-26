using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Recover : ActionBehavior
    {
        public Recover(GameObject go) : base(go)
        {
        }

        public override Status Step(Stack<Behavior> memory, Status message)
        {
            if (DriverObject.GetComponent<Attributes>().Health > 75) {
                Debug.Log("Recovered and resuming activity");
                var nextState = DriverObject.GetComponent<CivilianBehaviorFactory>().GetState(typeof(CivilianIdle));
                memory.Push((CivilianIdle) nextState);
            }
            DriverObject.GetComponent<Attributes>().Health += 10f * Time.deltaTime;
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
