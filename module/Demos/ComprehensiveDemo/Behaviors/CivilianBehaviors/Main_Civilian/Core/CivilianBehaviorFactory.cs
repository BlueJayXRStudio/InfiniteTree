using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InfiniteTree
{
    // Factory / Flyweight Factory
    public class CivilianBehaviorFactory : MonoBehaviour
    {
        Dictionary<string, Behavior> BehaviorCache = new();
        Dictionary<Type, object> StateCache = new();

        public object GetState(Type type, GameObject go) {
            if (!StateCache.ContainsKey(type)) {
                object instance = Activator.CreateInstance(type, new object[] { go });
                StateCache.Add(type, instance);
            }

            return StateCache[type];
        }
    }
}
