using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class a_ToWaypoints : Behavior
    {
        private float velocity = 1.0f;
        private int index = 0;
        private List<(int, int)> waypoints;

        public a_ToWaypoints(List<(int, int)> waypoints, GameObject go) : base(go) {
            this.waypoints = waypoints;
            DriverObject = go;
            velocity = go.GetComponent<Attributes>().MoveSpeed;
        }

        public override Status Step(Stack<Behavior> memory, GameObject go, Status message)
        {    
            memory.Push(this);

            var result = TreeRequirement(memory);
            if (result != Status.RUNNING) {
                return result;
            }
            
            if (index == waypoints.Count) {
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

            
            return Status.RUNNING;
        }

        public void Reset(List<(int, int)> waypoints) {
            this.waypoints = waypoints;
            index = 0;
        }

        private Vector3 contruct_position ((int, int) wp) => new Vector3(wp.Item1, DriverObject.transform.position.y, wp.Item2);

        public override Status CheckRequirement()
        {
            throw new System.NotImplementedException();
        }
    }
}
