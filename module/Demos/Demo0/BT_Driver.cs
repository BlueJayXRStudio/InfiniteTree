using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Driver : MonoBehaviour
{
    public List<GameObject> Waypoints;
    TaskStackMachine tree;
    Behavior initialAction;

    void Start()
    {
        gameObject.GetComponent<ParentComponent>().SetParents(Waypoints);

        tree = new(gameObject);
        initialAction = new FollowParent(gameObject);
        tree.Memory.Push(initialAction);
    }

    void Update()
    {
        tree.Drive();
        // We can opt to continue pushing FollowParent action if completed. The ouroboros.
        if (tree.Memory.Count == 0) tree.Memory.Push(initialAction);
    }
}
