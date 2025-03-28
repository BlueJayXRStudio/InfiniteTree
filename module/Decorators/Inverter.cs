using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Behavior
{
    Behavior ToInvert;
    public Inverter(Behavior toInvert) {
        ToInvert = toInvert;
    }

    public Status Step(Stack<Behavior> memory, Status message)
    {
        if (message == Status.RUNNING) {
            memory.Push(this);
            memory.Push(ToInvert);
            return Status.RUNNING;
        }
        else if (message == Status.SUCCESS) {
            memory.Push(this);
            return Status.FAILURE;
        }
        else {
            memory.Push(this);
            return Status.SUCCESS;
        }
    }
}
