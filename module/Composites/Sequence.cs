using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Behavior
{
    Queue<Behavior> Actions = new();
    List<Behavior> PrevActions = new();

    public Sequence(List<Behavior> ToPopulate, GameObject go) : base (go) {
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

        var nextAction = Actions.Dequeue();
        PrevActions.Add(nextAction);
        memory.Push(nextAction);
        return Status.RUNNING;
    }

    // If any of the previous nodes' conditions would currently fail, then
    // the sequence should ideally fail now. As in, past conditions should
    // persist over the sequence.
    public override Status CheckFailure()
    {
        for (int i = 0; i < PrevActions.Count - 1; i++) {
            var prevSuccess = PrevActions[i].CheckSuccess();

            if (prevSuccess == Status.RUNNING)
                return Status.FAILURE;
        }
        
        return Status.RUNNING;
    }

    // Sequence is trivially always running. As long the running node is a 
    // child of the sequence, then the sequence is running. 
    public override Status CheckSuccess()
    {
        return Status.RUNNING;
    }
}
