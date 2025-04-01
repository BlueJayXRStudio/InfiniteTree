using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Behavior
{
    protected bool Finished = false;
    protected Queue<Behavior> Actions = new();
    protected  List<Behavior> PrevActions = new();

    public Sequence(List<Behavior> ToPopulate, GameObject go) : base (go) {
        if (ToPopulate == null) return;
        foreach (Behavior action in ToPopulate) {
            Actions.Enqueue(action);
        }
    }

    public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {
        memory.Push(this);

        if (message == Status.FAILURE) {
            Finished = true;
            return Status.FAILURE;
        }
        if (Actions.Count == 0) {
            Finished = true;
            return Status.SUCCESS;
        }

        var nextAction = Actions.Dequeue();
        PrevActions.Add(nextAction);
        memory.Push(nextAction);
        return Status.RUNNING;
    }

    public override Status CheckRequirement()
    {
        for (int i = 0; i < PrevActions.Count - Convert.ToInt32(!Finished); i++) {
            var result = PrevActions[i].CheckRequirement();
            if (result != Status.SUCCESS)
                return Status.FAILURE;
        }
        if (!Finished)
            return Status.RUNNING;
        return Status.SUCCESS;
    }


}
