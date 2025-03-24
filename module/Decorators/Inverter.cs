using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Behavior
{
    Behavior ToInvert;
    public Inverter(Behavior toInvert) {
        ToInvert = toInvert;
    }

    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {
        if (message == Status.RUNNING) {
            memory.Push((this, Status.RUNNING));
            memory.Push((ToInvert, Status.RUNNING));
        }
        else if (message == Status.SUCCESS) {
            memory.Push((this, Status.FAIL));
        }
        else if (message == Status.FAIL) {
            memory.Push((this, Status.SUCCESS));
        }
    }
}
