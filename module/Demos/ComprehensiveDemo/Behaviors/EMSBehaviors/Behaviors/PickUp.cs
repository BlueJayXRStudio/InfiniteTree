using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class PickUp : Behavior
    {
        public GameObject Patient;

        public PickUp(GameObject patient) : base(null) {
            Patient = patient;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            Patient.transform.position = new Vector3(go.transform.position.x ,Patient.transform.position.y, go.transform.position.z);
            Patient.transform.SetParent(go.transform);
            
            memory.Push(this);
            return Status.SUCCESS;
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }
    }
}