using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_1_Driver : MonoBehaviour
{
    public List<GameObject> Waypoints;
    TaskStackMachine tree;

    void Start()
    {
        tree = new(gameObject);
        List<Behavior> Test_Sequence = new();
        foreach (GameObject go in Waypoints) {
            Test_Sequence.Add(new Inverter(new Inverter(new ToWaypoint(go, gameObject))));
        }
        tree.Memory.Push(new Sequence(Test_Sequence));
    }

    void Update()
    {
        var result = tree.Drive();

        if (result != Status.RUNNING)
            Debug.Log(result);

        // We can opt to continue pushing FollowParent action if completed. The ouroboros.
        // if (tree.Memory.Count == 0) tree.Memory.Push((initialAction, Status.RUNNING));
    }
}
