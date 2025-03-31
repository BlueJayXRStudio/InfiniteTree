using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class ToWaypoints : Behavior
    {
        private float velocity = 1.0f;
        private int index = 0;
        private List<(int, int)> waypoints;
        private GameObject DriverObject;

        public ToWaypoints(List<(int, int)> waypoints, GameObject go) {
            this.waypoints = waypoints;
            DriverObject = go;
            velocity = go.GetComponent<Attributes>().MoveSpeed;
        }

        public Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {    
            // Here we are remembering why we are currently moving to a waypoint, and
            // will exit if we no longer need to. We delegate the stack traversal to
            // a static method ShouldTerminate
            if (EarlyTerminator.ShouldTerminate(memory) != Status.RUNNING) {
                memory.Push(this);
                return EarlyTerminator.ShouldTerminate(memory);
            }
            
            if (index == waypoints.Count) {
                memory.Push(this);
                return Status.SUCCESS;
            }

            Vector3 ParentPos = contruct_position(waypoints[index]);
            Vector3 CurrentPos = DriverObject.transform.position;
            Vector3 diff = ParentPos - CurrentPos;

            if (diff.magnitude >= 0.02f) {
                DriverObject.transform.rotation = Quaternion.LookRotation(diff, Vector3.up);
                DriverObject.transform.position += diff.normalized * velocity * Time.deltaTime;
            }
            else
                index++;

            memory.Push(this);
            return Status.RUNNING;
        }

        public void Reset(List<(int, int)> waypoints) {
            this.waypoints = waypoints;
            index = 0;
        }

        private Vector3 contruct_position ((int, int) wp) => new Vector3(wp.Item1, DriverObject.transform.position.y, wp.Item2);
    }
}
