using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior
{
    public GameObject DriverObject;
    public Behavior(GameObject go) => DriverObject = go;

    public abstract Status Step(Stack<Behavior> memory, GameObject go, Status message);
    public abstract Status CheckRequirement();

    public Status TreeRequirement(Stack<Behavior> memory) {
        Stack<Behavior> tempStack = new();
        Status result = Status.RUNNING;
        
        while (memory.Count > 0)
        {
            var task = memory.Pop();
            tempStack.Push(task);
            result = task.CheckRequirement();
            if (result != Status.RUNNING) break;
        }

        while (tempStack.Count > 0)
            memory.Push(tempStack.Pop());

        return result;
    }
}
