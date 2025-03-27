using System.Collections;
using System.Collections.Generic;
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
        memory.Push(this);

        if (message == Status.FAIL) 
            return Status.FAIL;

        else if (Actions.Count == 0) 
            return Status.SUCCESS;
            
        else {
            memory.Push(Actions.Dequeue());
            return Status.RUNNING;
        }
    }
}
