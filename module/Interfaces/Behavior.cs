using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Behavior
{
    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message);
}
