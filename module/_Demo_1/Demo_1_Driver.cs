using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_1_Driver : MonoBehaviour
{
    public List<GameObject> Waypoints;
    BehaviorTree tree;

    void Start()
    {
        tree = new(gameObject);
        List<(Behavior, Status)> Test_Sequence = new();
        foreach (GameObject go in Waypoints) {
            Test_Sequence.Add((new Inverter(new Inverter(new ToWaypoint(go))), Status.RUNNING));
        }
        tree.Memory.Push((new Sequence(Test_Sequence), Status.RUNNING));
    }

    void Update()
    {
        Status result = tree.Drive();

        if (result == Status.FAIL || result == Status.SUCCESS) {
            Debug.Log(result);
        }

        // We can opt to continue pushing FollowParent action if completed. The ouroboros.
        // if (tree.Memory.Count == 0) tree.Memory.Push((initialAction, Status.RUNNING));
    }
}
