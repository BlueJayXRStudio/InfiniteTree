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
    private Status Message = Status.SUCCESS;

    public TaskStackMachine(GameObject go) {
        DriverObject = go;
        Memory = new();
    }

    public Status Drive() {
        if (Memory.Count == 0) return Message;

        Behavior CurrentAction = Memory.Pop();

        if (Message == Status.RUNNING)
            Message = CurrentAction.Step(Memory, Status.RUNNING);
        
        else {
            if (Memory.Count == 0) return Message;
            Message = Memory.Pop().Step(Memory, Message);
        }

        return Message;
    }

    public void AddBehavior(Behavior behavior) {
        Memory.Push(behavior);
        Message = Status.RUNNING;
    }

    public Status GetMessage() => Message;
}
