// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RotateBehavior : Behavior
// {
//     public Status Step(Stack<Behavior> memory, GameObject go, Status message)
//     {
//         go.transform.rotation = Quaternion.Euler(0, 180 * Time.deltaTime, 0) * go.transform.rotation;
//         memory.Push(this);
//         return Status.RUNNING;
//     }
// }
