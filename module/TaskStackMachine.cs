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
    private Status LastMessage = Status.SUCCESS;

    public TaskStackMachine(GameObject go) {
        DriverObject = go;
        Memory = new();
    }

    public Status Drive() {
        if (Memory.Count == 0) return LastMessage;

        Behavior CurrentAction = Memory.Pop();

        if (LastMessage == Status.RUNNING)
            LastMessage = CurrentAction.Step(Memory, Status.RUNNING);
        
        else if (Memory.Count > 0) 
            LastMessage = Memory.Pop().Step(Memory, LastMessage);
        
        return LastMessage;
    }

    public void AddBehavior(Behavior behavior) {
        Memory.Push(behavior);
        LastMessage = Status.RUNNING;
    }

    public Status GetLastMessage() => LastMessage;
}
