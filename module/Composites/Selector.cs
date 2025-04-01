using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Behavior
{
    protected bool Finished = false;
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
            Finished = true;
            return Status.SUCCESS;
        }
        else if (Actions.Count == 0) {
            Finished = true;
            return Status.FAILURE;
        }
        else {
            memory.Push(Actions.Dequeue());
            return Status.RUNNING;
        }
    }

    public override Status CheckRequirement()
    {
        for (int i = 0; i < PrevActions.Count - Convert.ToInt32(!Finished); i++) {
            var result = PrevActions[i].CheckRequirement();
            if (result != Status.FAILURE)
                return Status.SUCCESS;
        }
        if (!Finished)
            return Status.RUNNING;
        return Status.FAILURE;
    }

}