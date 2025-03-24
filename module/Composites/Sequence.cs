using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Sequence : Behavior
{
    Queue<Behavior> Actions;

    public Sequence(List<Behavior> ToPopulate) {
        Actions = new();
        foreach (Behavior action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public Status Step(Stack<Behavior> memory, Status message)
    {
        // Debug.Log("Iterating Sequence");
        if (message == Status.FAIL) {
            memory.Push(this);
            return Status.FAIL;
        }
        else if (Actions.Count == 0) {
            memory.Push(this);
            return Status.SUCCESS;
        }
        else {
            memory.Push(this);
            memory.Push(Actions.Dequeue());
            return Status.RUNNING;
        }
    }
}
