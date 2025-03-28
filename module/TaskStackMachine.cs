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

    // // Basic Logical Structure of the Task Stack Machine
    // public Status Drive() {

    //     if (Memory.Count == 0) return Status.RUNNING;

    //     if (Message == Status.RUNNING) {
    //         Message = Memory.Pop().Step(Memory, DriverObject, Message);
    //         return Message;
    //     }
    //     else {
    //         // A caller is expecting a message
    //         if (Memory.Count > 1) {
    //             Memory.Pop();
    //             Message = Memory.Pop().Step(Memory, DriverObject, Message);
    //             return Message;
    //         }
    //         // No caller, program can exit (await new tasks)
    //         else {
    //             Memory.Pop();
    //             return Message;
    //         }
    //     }
    // }

    public Status Drive() {
        if (Memory.Count == 0) return Status.RUNNING;

        var SubTask = Memory.Pop();

        if (Message == Status.RUNNING) {
            Message = SubTask.Step(Memory, DriverObject, Message);
            return Message;
        }

        else if (Memory.Count > 0) {
            Message = Memory.Pop().Step(Memory, DriverObject, Message);
            return Message;
        }

        return Status.RUNNING;
    }

    public void AddBehavior(Behavior behavior) {
        Memory.Push(behavior);
        Message = Status.RUNNING;
    }

    public Status GetMessage => Message;
    public int GetStackCount => Memory.Count;
    public bool ProgramFinish => Message != Status.RUNNING && Memory.Count == 1;
}
