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
        Dictionary<string, object> StateCache = new();
        
        // For most of these factory methods, we can rely on lazy instantiation.
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

        public object GetState(string name) {
            if (!StateCache.ContainsKey(name)) {
                Type type = (Type.GetType(name) 
                    ?? Assembly.GetExecutingAssembly().GetType(name)) 
                    ?? throw new System.Exception($"Type {name} does not exist");

                object instance = Activator.CreateInstance(type);
                StateCache.Add(name, instance);
            }
            
            return StateCache[name];
        }
    }
}
