using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : Behavior
{
    private float velocity = 1.0f;
    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {
        GameObject parent = go.GetComponent<ParentComponent>().GetParent;

        if (parent == null) memory.Push((this, Status.SUCCESS));

        Vector3 ParentPos = parent.transform.position;
        Vector3 CurrentPos = go.transform.position;

        Vector3 diff = ParentPos - CurrentPos;

        if (diff.magnitude >= 0.02f) {
            go.transform.position += diff.normalized * velocity * Time.deltaTime;
            memory.Push((this, Status.RUNNING));
        }
        else {
            // go.transform.position = parent.transform.position;
            go.GetComponent<ParentComponent>().iterate();
            memory.Push((this, Status.RUNNING));
        }
    }
}
