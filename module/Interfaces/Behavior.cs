using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Behavior
{
    public Status Step(Stack<Behavior> memory, GameObject go, Status message);
}

public interface ICheckTermination
{
    public Status CheckTermination();
}