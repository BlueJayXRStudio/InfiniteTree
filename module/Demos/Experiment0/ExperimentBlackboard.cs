using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentBlackboard : Blackboard<ExperimentBlackboard>
{
    Dictionary<(int, int), string> map;

    void Awake()
    {
        map = new();
    }
}
