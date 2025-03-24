using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Sequence : Behavior
{
    Queue<(Behavior, Status)> Actions;

    public Sequence(List<(Behavior, Status)> ToPopulate) {
        Actions = new();
        foreach ((Behavior, Status) action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {
        // Debug.Log("Iterating Sequence");
        if (message == Status.FAIL) {
            memory.Push((this, Status.FAIL));
        }
        else if (Actions.Count == 0) {
            memory.Push((this, Status.SUCCESS));
        }
        else {
            memory.Push((this, Status.RUNNING));
            memory.Push((Actions.Dequeue().Item1, Status.RUNNING));
        }
    }
}
