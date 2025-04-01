// using System.Collections.Generic;
// using UnityEngine;

// namespace InfiniteTree
// {
//     public class DropOff : Behavior
//     {
//         private GameObject Patient;

//         public DropOff(GameObject patient) {
//             Patient = patient;
//         }

//         public Status Step(Stack<Behavior> memory, GameObject go, Status message)
//         {
//             Patient.transform.SetParent(null);
//             Patient.GetComponent<CivilianAttributes>().ForceWake = true;

//             memory.Push(this);            
//             return Status.SUCCESS;
//         }
//     }
// }