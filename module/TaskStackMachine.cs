using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status 
{
    SUCCESS,
    RUNNING,
    FAILURE
}

public class TaskStackMachine
{
    public GameObject DriverObject;
    public Stack<Behavior> Memory;
    private Status Message = Status.RUNNING;

    public TaskStackMachine(GameObject go) {
        DriverObject = go;
        Memory = new();
    }

    public Status Drive() {
        if (Memory.Count == 0) return Status.RUNNING;

        Behavior CurrentAction = Memory.Pop();

        if (Message == Status.RUNNING)
            Message = CurrentAction.Step(Memory, DriverObject, Message);
        
        else if (Memory.Count > 0)
            Message = Memory.Pop().Step(Memory, DriverObject, Message);
        
        return Message;
    }

    public void AddBehavior(Behavior behavior) {
        Memory.Push(behavior);
        Message = Status.RUNNING;
    }

    public Status GetMessage() => Message;
    public int GetStackCount() => Memory.Count;
}
