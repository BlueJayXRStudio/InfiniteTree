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

        while (memory.Count > 1)
            tempStack.Push(memory.Pop());

        var root = memory.Pop();
        tempStack.Push(root);
        var result = root.CheckRequirement();

        while (tempStack.Count > 0)
            memory.Push(tempStack.Pop());

        return result;
    }
}
