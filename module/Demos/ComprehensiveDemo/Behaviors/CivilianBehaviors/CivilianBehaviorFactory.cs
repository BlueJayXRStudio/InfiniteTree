using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianBehaviorFactory : MonoBehaviour
    {
        Dictionary<string, Behavior> BehaviorCache = new();
        public ToWaypoints GetMoveBehavior(GameObject go, (int, int) destination) {
            var waypoints = ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, go.GetComponent<Attributes>().GetPos, destination);

            if (BehaviorCache.ContainsKey("ToWaypoints")) {
                ((ToWaypoints) BehaviorCache["ToWaypoints"]).SetWaypoints(waypoints);
                return (ToWaypoints) BehaviorCache["ToWaypoints"];
            }
            
            var newToWaypoints = new ToWaypoints(waypoints, go);
            BehaviorCache.Add("ToWaypoints", newToWaypoints);
            return newToWaypoints;
        }
    }
}
