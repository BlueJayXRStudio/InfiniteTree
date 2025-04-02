using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SUCCESS OR FAIL ON ANY
public class Parallel : Behavior
{
    List<TaskStackMachine> trees;

    public Parallel(List<Behavior> ParallelActions, GameObject go) : base(go) {
        trees = new();
        foreach (Behavior action in ParallelActions) {
            var tree = new TaskStackMachine(null);
            tree.AddBehavior(action);
            trees.Add(tree);
        }
    }

    public override Status CheckRequirement()
    {
        throw new System.NotImplementedException();
    }

    public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {
        // int fail_count = 0;
        foreach (TaskStackMachine tree in trees) {
            tree.DriverObject = go;
            var result = tree.Drive();
            if (result != Status.RUNNING) {
                memory.Push(this);
                return result;
            }
            // else if (result == Status.FAILURE) {
            //     fail_count++;
            // }
        }

        // if (fail_count == trees.Count) {
        //     memory.Push(this);
        //     return Status.FAILURE;
        // }
        
        memory.Push(this);
        return Status.RUNNING;
    }
}
