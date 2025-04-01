// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Demo_2_Driver : MonoBehaviour
// {
//     public List<GameObject> Waypoints;
//     TaskStackMachine tree;

//     void Start()
//     {
//         tree = new(gameObject);
//         List<Behavior> Test_Sequence = new();
//         int partition = 2;

//         for (int i = 0; i < partition; i++) {
//             Test_Sequence.Add(new Parallel(new List<Behavior>() { new ToWaypoint(Waypoints[i]), new RotateBehavior() }, gameObject));
//         }
//         for (int i = partition; i < Waypoints.Count; i++) {
//             Test_Sequence.Add(new ToWaypoint(Waypoints[i]));
//         }

//         tree.AddBehavior(new Sequence(Test_Sequence));
        
//         // tree.AddBehavior(new ToWaypoint(Waypoints[2]));
//         // Debug.Log("pushing new behavior");
//         // Debug.Log(tree.GetMessage());
//     }

//     void Update()
//     {
//         Status result = tree.Drive();

//         if (tree.ProgramFinish)
//             Debug.Log($"result: {tree.GetMessage}, returned message: {result}");

//         // We can opt to continue pushing FollowParent action if completed. The ouroboros.
//         // if (tree.Memory.Count == 0) tree.Memory.Push((initialAction, Status.RUNNING));
//     }
// }
