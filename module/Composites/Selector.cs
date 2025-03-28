using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Behavior
{
    Queue<Behavior> Actions;

    public Selector(List<Behavior> ToPopulate) {
        Actions = new();
        foreach (Behavior action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public Status Step(Stack<Behavior> memory, Status message)
    {
        if (message == Status.SUCCESS) {
            memory.Push(this);
            return Status.SUCCESS;
        }
        else if (Actions.Count == 0) {
            memory.Push(this);
            return Status.FAILURE;
        }
        else {
            memory.Push(this);
            memory.Push(Actions.Dequeue());
            return Status.RUNNING;
        }
    }
}