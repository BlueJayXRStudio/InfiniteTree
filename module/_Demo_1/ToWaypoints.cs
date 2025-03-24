using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToWaypoint : Behavior
{
    private float velocity = 1.0f;
    private GameObject waypoint;

    public ToWaypoint(GameObject waypoint) => this.waypoint = waypoint;

    public void Step(GameObject go, Stack<(Behavior, Status)> memory, Status message)
    {        
        // Debug.Log("Traveling to Way Point");
        Vector3 ParentPos = waypoint.transform.position;
        Vector3 CurrentPos = go.transform.position;

        Vector3 diff = ParentPos - CurrentPos;

        if (!waypoint.activeSelf) {
            memory.Push((this, Status.FAIL));
            return;
        }

        if (diff.magnitude >= 0.02f) {
            go.transform.position += diff.normalized * velocity * Time.deltaTime;
            memory.Push((this, Status.RUNNING));
        }
        else {
            // go.transform.position = parent.transform.position;
            memory.Push((this, Status.SUCCESS));
        }
    }
}
