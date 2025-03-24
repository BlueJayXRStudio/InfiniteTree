using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : Behavior
{
    GameObject DriverObject;

    public RotateBehavior(GameObject go) => DriverObject = go;

    public Status Step(Stack<Behavior> memory, Status message)
    {
        DriverObject.transform.rotation = Quaternion.Euler(0, 180 * Time.deltaTime, 0) * DriverObject.transform.rotation;
        memory.Push(this);
        return Status.RUNNING;
    }
}
