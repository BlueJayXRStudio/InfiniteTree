using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Behavior
{
    Queue<Behavior> Actions;
    List<Behavior> PrevActions;

    public Sequence(List<Behavior> ToPopulate, GameObject go) : base (go) {
        Actions = new();
        foreach (Behavior action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {
        memory.Push(this);

        if (message == Status.FAILURE) 
            return Status.FAILURE;

        if (Actions.Count == 0)
            return Status.SUCCESS;

        memory.Push(Actions.Dequeue());
        return Status.RUNNING;
    }

    public override Status CheckFailure()
    {
        throw new System.NotImplementedException();
    }

    public override Status CheckSuccess()
    {
        if (Actions.Count == 0)
            return Status.SUCCESS;
    }
}
