using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EMSBehaviorFactory : MonoBehaviour
    {
        Dictionary<string, Behavior> BehaviorCache = new();
        Dictionary<Type, object> StateCache = new();

        public object GetState(Type type) {
            if (!StateCache.ContainsKey(type)) {
                object instance = Activator.CreateInstance(type, new object[] { });
                StateCache.Add(type, instance);
            }

            return StateCache[type];
        }
    }
}
