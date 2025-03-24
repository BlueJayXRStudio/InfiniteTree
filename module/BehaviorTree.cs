using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status 
{
    SUCCESS,
    RUNNING,
    FAIL
}

public class BehaviorTree
{
    public GameObject DriverObject;
    public Stack<(Behavior, Status)> Memory;
    private Status Result;

    public BehaviorTree(GameObject go) {
        DriverObject = go;
        Memory = new();
    }

    public Status Drive() {
        if (Memory.Count == 0) return Result;

        (Behavior, Status) CurrentAction = Memory.Pop();

        if (CurrentAction.Item2 == Status.RUNNING) {
            CurrentAction.Item1.Step(DriverObject, Memory, Status.RUNNING);
        }
        else if (CurrentAction.Item2 == Status.SUCCESS) {
            if (Memory.Count == 0) {
                Result = Status.SUCCESS;
                return Status.SUCCESS;
            }
            Memory.Pop().Item1.Step(DriverObject, Memory, Status.SUCCESS);
        } 
        else if (CurrentAction.Item2 == Status.FAIL) {
            if (Memory.Count == 0) {
                Result = Status.FAIL;
                return Status.FAIL;
            }
            Memory.Pop().Item1.Step(DriverObject, Memory, Status.FAIL);
            
        }
        return Status.RUNNING;
    }
}
