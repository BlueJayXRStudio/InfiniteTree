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

    public Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {
        memory.Push(this);
        
        if (message == Status.SUCCESS) {
            return Status.SUCCESS;
        }
        else if (Actions.Count == 0) {
            return Status.FAILURE;
        }
        else {
            memory.Push(Actions.Dequeue());
            return Status.RUNNING;
        }
    }
}