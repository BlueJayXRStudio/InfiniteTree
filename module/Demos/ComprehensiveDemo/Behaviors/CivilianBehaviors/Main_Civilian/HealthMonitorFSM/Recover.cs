using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class Recover : Behavior
    {
        public Recover(GameObject go) : base(go)
        {
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            if (go.GetComponent<CivilianAttributes>().ForceWake)
                go.GetComponent<CivilianAttributes>().ForceWake = false;

            if (go.GetComponent<Attributes>().Health > 75) {
                Debug.Log("Recovered and resuming activity");
                var nextState = go.GetComponent<CivilianBehaviorFactory>().GetState(typeof(CivilianIdle), go);
                memory.Push((CivilianIdle) nextState);
                go.GetComponent<CivilianDriver>().SwitchTree();
                go.GetComponent<CivilianDriver>().ResetTree();
                return Status.RUNNING;
            }
            go.GetComponent<Attributes>().Health += 10f * Time.deltaTime;
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
