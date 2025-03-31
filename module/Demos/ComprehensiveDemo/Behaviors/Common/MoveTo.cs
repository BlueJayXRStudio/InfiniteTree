using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class MoveTo : Behavior
    {
        GameObject DriverObject;
        ToWaypoints moveTo;
        (int, int) destination;
        public MoveTo(GameObject go, (int, int) dest) {
            DriverObject = go;
            destination = dest;
        }

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (message == Status.SUCCESS)
                return Status.SUCCESS;
                
            var waypoints = ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, go.GetComponent<Attributes>().GetPos, destination);
            moveTo ??= new ToWaypoints(waypoints, go);

            memory.Push(moveTo);
            return Status.RUNNING;
        }
    }
}