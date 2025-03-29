using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarlyTerminator
{
    
    /// <summary>
    /// This kind of mimics our way of thinking. When we're doing a specific
    /// subtask, we periodically recall previous tasks (callers), and 
    /// see if those tasks' requirements are already met. If so, we can
    /// simply terminate our current subtask. But, of course, complex
    /// behaviors rise from having the option to terminate or not terminate
    /// based on prior requirements, hence the choice of naming "Should
    /// Terminate". We will return Status message rather than a boolean,
    /// because termination is determined by both SUCCESS and FAILURE, which
    /// are symmetric in some sense, but not entirely equivalent.
    /// </summary>
    /// <param name="memory">Stack Memory</param>
    /// <returns>Status Message</returns>
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
