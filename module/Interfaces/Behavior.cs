using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior : IRequirement
{
    public GameObject DriverObject;
    public Behavior(GameObject go) => DriverObject = go;

    public abstract Status Step(Stack<Behavior> memory, GameObject go, Status message);
    public abstract Status CheckSuccess();
    public abstract Status CheckFailure();
}

// "Why" are we doing what we're doing?
public interface IRequirement
{
    public Status CheckSuccess();
    public Status CheckFailure();
}