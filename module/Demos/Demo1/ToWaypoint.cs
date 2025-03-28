using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToWaypoint : Behavior
{
    private float velocity = 1.0f;
    private GameObject waypoint;

    public ToWaypoint(GameObject waypoint) {
        this.waypoint = waypoint;
    }

    public Status Step(Stack<Behavior> memory, GameObject go, Status message)
    {        
        // Debug.Log("Traveling to Way Point");
        Vector3 ParentPos = waypoint.transform.position;
        Vector3 CurrentPos = go.transform.position;

        Vector3 diff = ParentPos - CurrentPos;

        if (!waypoint.activeSelf) {
            memory.Push(this);
            return Status.FAILURE;
        }

        if (diff.magnitude >= 0.02f) {
            go.transform.position += diff.normalized * velocity * Time.deltaTime;
            memory.Push(this);
            return Status.RUNNING;
        }
        else {
            // go.transform.position = parent.transform.position;
            memory.Push(this);
            return Status.SUCCESS;
        }
    }
}
