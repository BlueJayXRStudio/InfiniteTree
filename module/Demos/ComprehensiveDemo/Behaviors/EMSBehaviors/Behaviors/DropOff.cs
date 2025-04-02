using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class DropOff : Behavior
    {
        private GameObject Patient;

        public DropOff(GameObject patient) : base(null) {
            Patient = patient;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            Patient.transform.SetParent(null);
            Patient.GetComponent<CivilianAttributes>().ForceWake = true;

            memory.Push(this);            
            return Status.SUCCESS;
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }
    }
}