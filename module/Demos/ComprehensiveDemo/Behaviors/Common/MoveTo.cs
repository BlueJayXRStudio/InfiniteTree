using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class MoveTo : Behavior
    {
        a_ToWaypoints moveTo;
        (int, int) destination;

        bool with_req = true;
        bool Finished = false;

        public MoveTo(GameObject go, (int, int) dest) : base(go) {
            DriverObject = go;
            destination = dest;
        }

        public MoveTo(GameObject go, (int, int) dest, bool with_req) : base(go) {
            DriverObject = go;
            destination = dest;
            this.with_req = with_req;
        }

        public override Status CheckRequirement()
        {
            if (!Finished) return Status.RUNNING;
            return Status.FAILURE;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {
            memory.Push(this);

            if (message != Status.RUNNING) {
                Finished = true;
                return message;
            }

            var waypoints = ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, go.GetComponent<Attributes>().GetPos, destination);
            moveTo ??= !with_req ? new a_ToWaypoints(waypoints, go, with_req) : new a_ToWaypoints(waypoints, go);

            memory.Push(moveTo);
            return Status.RUNNING;
        }

    }
}