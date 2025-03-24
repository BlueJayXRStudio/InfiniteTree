using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentBlackboard : Blackboard<ExperimentBlackboard>
{
    public Dictionary<(int, int), string> map = new();
}
