using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : Behavior
{
    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {
        go.transform.rotation = Quaternion.Euler(0, 180 * Time.deltaTime, 0) * go.transform.rotation;
        memory.Push((this, Status.RUNNING));
    }
}
