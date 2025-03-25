using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteTree;
using UnityEngine;

public class CivilianDriver : MonoBehaviour
{
    BehaviorTree tree;

    void Awake()
    {
        tree = new(gameObject);
    }

    async void Start() 
    {
        // while (ExperimentBlackboard.Instance.map == null) await Task.Yield();
        tree.Memory.Push(new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, (0,0), (7,7)), gameObject));
    }

    void Update()
    {
        Debug.Log(tree.Drive());
    }
}
