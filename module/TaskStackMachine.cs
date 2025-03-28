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
    private Status Result = Status.SUCCESS;

    public TaskStackMachine(GameObject go) {
        DriverObject = go;
        Memory = new();
    }

    public Status Drive() {
        if (Memory.Count == 0) return Result;

        Behavior CurrentAction = Memory.Pop();

        if (Message == Status.RUNNING) {
            Message = CurrentAction.Step(Memory, Status.RUNNING);
        }
        else if (Message == Status.SUCCESS) {
            if (Memory.Count == 0) {
                Result = Status.SUCCESS;
                return Status.SUCCESS;
            }
            Message = Memory.Pop().Step(Memory, Status.SUCCESS);
        } 
        else {
            if (Memory.Count == 0) {
                Result = Status.FAILURE;
                return Status.FAILURE;
            }
            Message = Memory.Pop().Step(Memory, Status.FAILURE);
        }
        return Status.RUNNING;
    }

    public void AddBehavior(Behavior behavior) {
        Memory.Push(behavior);
        Message = Status.RUNNING;
    }
}
