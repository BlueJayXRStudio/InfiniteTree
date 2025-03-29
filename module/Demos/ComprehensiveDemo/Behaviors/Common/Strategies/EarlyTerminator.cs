using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarlyTerminator
{
    GameObject DriverObject;
    Stack<Behavior> Memory;

    public EarlyTerminator(Stack<Behavior> memory, GameObject go) {
        Memory = memory;
        DriverObject = go;
    }

    public Status ShouldTerminate() {
        Status result = Status.RUNNING;

        Stack<Behavior> tempStack = new();

        while (Memory.Count > 0) {
            var CurrentAction = Memory.Pop();
            tempStack.Push(CurrentAction);

            if (CurrentAction is ICheckTermination terminable) {
                result = terminable.CheckTermination();
                if (result != Status.RUNNING) break;
            }
        }

        while (tempStack.Count > 0)
            Memory.Push(tempStack.Pop());

        return result;
    }
}
