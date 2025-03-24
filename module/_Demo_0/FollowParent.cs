using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : Behavior
{
    private float velocity = 1.0f;
    GameObject DriverObject;

    public FollowParent(GameObject go) => DriverObject = go;

    public Status Step(Stack<Behavior> memory, Status message)
    {
        GameObject parent = DriverObject.GetComponent<ParentComponent>().GetParent;

        if (parent == null) {
            memory.Push(this);
            return Status.SUCCESS;
        }

        Vector3 ParentPos = parent.transform.position;
        Vector3 CurrentPos = DriverObject.transform.position;
        Vector3 diff = ParentPos - CurrentPos;

        if (diff.magnitude >= 0.02f) {
            DriverObject.transform.position += diff.normalized * velocity * Time.deltaTime;
            memory.Push(this);
            return Status.RUNNING;
        }
        else {
            // go.transform.position = parent.transform.position;
            DriverObject.GetComponent<ParentComponent>().iterate();
            memory.Push(this);
            return Status.RUNNING;
        }
    }
}
