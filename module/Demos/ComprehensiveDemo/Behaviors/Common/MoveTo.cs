using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class MoveTo : Behavior
    {
        a_ToWaypoints moveTo;
        (int, int) destination;

        public MoveTo(GameObject go, (int, int) dest) : base(go) {
            DriverObject = go;
            destination = dest;
        }

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (message != Status.RUNNING)
                return message;
                
            var waypoints = ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, go.GetComponent<Attributes>().GetPos, destination);
            moveTo ??= new a_ToWaypoints(waypoints, go);

            memory.Push(moveTo);
            return Status.RUNNING;
        }

    }
}