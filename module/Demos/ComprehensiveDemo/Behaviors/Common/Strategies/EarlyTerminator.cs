using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarlyTerminator
{
    public static Status ShouldTerminate(Stack<Behavior> memory) {
        Status result = Status.RUNNING;

        Stack<Behavior> tempStack = new();

        while (memory.Count > 0) {
            var CurrentAction = memory.Pop();
            tempStack.Push(CurrentAction);

            if (CurrentAction is ICheckTermination terminable) {
                result = terminable.CheckTermination();
                if (result != Status.RUNNING) break;
            }
        }

        while (tempStack.Count > 0)
            memory.Push(tempStack.Pop());

        return result;
    }
}
