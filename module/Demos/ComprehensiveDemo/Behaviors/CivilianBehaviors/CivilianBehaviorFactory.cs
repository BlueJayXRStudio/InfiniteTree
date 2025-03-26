using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InfiniteTree
{
    // Some kind of a hybrid between factory and flyweight
    public class CivilianBehaviorFactory : MonoBehaviour
    {
        Dictionary<string, Behavior> BehaviorCache = new();
        Dictionary<Type, object> StateCache = new();
        
        // For most of these factory methods, we can rely on lazy instantiation.
        public ToWaypoints GetMoveBehavior(GameObject go, (int, int) destination) {
            var waypoints = ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, go.GetComponent<Attributes>().GetPos, destination);

            if (BehaviorCache.ContainsKey("ToWaypoints")) {
                ((ToWaypoints) BehaviorCache["ToWaypoints"]).Reset(waypoints);
                return (ToWaypoints) BehaviorCache["ToWaypoints"];
            }

            var newToWaypoints = new ToWaypoints(waypoints, go);
            BehaviorCache.Add("ToWaypoints", newToWaypoints);
            return newToWaypoints;
        }

        public object GetState(Type type, GameObject go) {
            if (!StateCache.ContainsKey(type)) {
                object instance = Activator.CreateInstance(type, new object[] { go });
                StateCache.Add(type, instance);
            }

            return StateCache[type];
        }
    }
}
