using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Behavior
{
    Behavior ToInvert;
    public Inverter(Behavior toInvert) {
        ToInvert = toInvert;
    }

    public Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {
        memory.Push(this);

        if (message == Status.RUNNING) {
            memory.Push(ToInvert);
            return Status.RUNNING; }
        else if (message == Status.SUCCESS)
            return Status.FAILURE;
        else 
            return Status.SUCCESS;
    }
}
