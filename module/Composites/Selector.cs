using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Behavior
{
    protected Queue<Behavior> Actions = new();
    protected List<Behavior> PrevActions = new();

    public Selector(List<Behavior> ToPopulate, GameObject go) : base(go) {        
        if (ToPopulate == null) return;

        foreach (Behavior action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
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

    public override Status CheckFailure()
    {
        return Status.RUNNING;
    }

    public override Status CheckSuccess()
    {
        for (int i = 0; i < PrevActions.Count - 1; i++) {
            var prevSuccess = PrevActions[i].CheckSuccess();

            if (prevSuccess == Status.SUCCESS)
                return Status.SUCCESS;
        }
        
        return Status.RUNNING;
    }

}