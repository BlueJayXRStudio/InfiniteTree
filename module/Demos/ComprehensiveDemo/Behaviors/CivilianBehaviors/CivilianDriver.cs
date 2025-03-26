using System.Collections;
using System.Collections.Generic;
using InfiniteTree;
using UnityEngine;

public class CivilianDriver : MonoBehaviour
{
    BehaviorTree tree;

    void Awake()
    {
        tree = new(gameObject);
    }

    void Start() 
    {
        // tree.Memory.Push(
        //     new Sequence(new List<Behavior>() {
        //         new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, (3,2), (1,4)), gameObject),
        //         new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, (1,4), (7,7)), gameObject),
        //         new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, (7,7), (3,4)), gameObject),
        //         new ToWaypoints(ExperimentBlackboard.Instance.ShortestPath(ExperimentBlackboard.Instance.map, (3,4), (7,1)), gameObject)
        //     })
        // );
        tree.AddBehavior(new CivilianControlFlow(gameObject));
    }

    void Update()
    {
        tree.Drive();
    }
}
