// using System.Collections.Generic;
// using UnityEngine;

// namespace InfiniteTree
// {
//     public class GoToStoreBehavior : Behavior
//     {
//         private GameObject DriverObject;
//         public GoToStoreBehavior(GameObject go) => DriverObject = go;

//         public Status Step(Stack<Behavior> memory, GameObject go, Status message)
//         {
//             memory.Push(this);

//             if (EarlyTerminator.ShouldTerminate(memory) != Status.RUNNING) {
//                 return EarlyTerminator.ShouldTerminate(memory);
//             }

//             if (go.GetComponent<Attributes>().GetPos != ExperimentBlackboard.Instance.GroceryStorePos) {
//                 Debug.Log("Going to the grocery store!");

//                 memory.Push(
//                     go.GetComponent<CivilianBehaviorFactory>().GetMoveBehavior(go, 
//                         ExperimentBlackboard.Instance.GroceryStorePos)
//                 );

//                 return Status.RUNNING;
//             }
//             else {
//                 if (go.GetComponent<Attributes>().Cash < 15)
//                     return Status.FAILURE;
                    
//                 go.GetComponent<Attributes>().FoodItem += 1;
//                 go.GetComponent<Attributes>().Cash -= 15;
//                 return Status.SUCCESS;
//             }
//         }
//     }
// }