using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    // Eager instantiation
    void Awake() { instance = this as T; } // This works because "as" will typecast during runtime

    // We don't need to check for null, because we'll
    // just rely on eager instantiation.
    public static T Instance {
        get {
            return instance;
        }
    }
}