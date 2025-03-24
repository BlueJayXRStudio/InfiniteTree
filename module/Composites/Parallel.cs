using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SUCCESS ON ANY
public class Parallel : Behavior
{
    List<BehaviorTree> trees;

    public Parallel(List<Behavior> ParallelActions) {
        trees = new();
        foreach (Behavior action in ParallelActions) {
            var tree = new BehaviorTree(null);
            tree.Memory.Push((action, Status.RUNNING));
            trees.Add(tree);
        }
    }
    
    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {
        int fail_count = 0;
        foreach (BehaviorTree tree in trees) {
            tree.DriverObject = go;
            var result = tree.Drive();
            if (result == Status.SUCCESS) {
                memory.Push((this, Status.SUCCESS));
                return;
            }
            else if (result == Status.FAIL) {
                fail_count++;
            }
        }

        if (fail_count == trees.Count) {
            memory.Push((this, Status.FAIL));
            return;
        }
        
        memory.Push((this, Status.RUNNING));
    }
}
