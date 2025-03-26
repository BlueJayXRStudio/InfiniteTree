using System.Collections;
using System.Collections.Generic;
using InfiniteTree;
using UnityEngine;

public class CivilianDriver : MonoBehaviour
{
    BehaviorTree tree;
    BehaviorTree HealthStates;

    bool treePaused = false;
    bool HealthStatesPaused = false;

    void Awake()
    {
        tree = new(gameObject);
        HealthStates = new(gameObject);
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
        HealthStates.AddBehavior((CivilianIdle) GetComponent<CivilianBehaviorFactory>().GetState(typeof(CivilianIdle), gameObject));
    }

    void Update()
    {
        if (!treePaused) tree.Drive();
        if (!HealthStatesPaused) HealthStates.Drive();
    }

    public void ResetTree()
    {
        tree.Memory = new Stack<Behavior>();
        tree.AddBehavior(new CivilianControlFlow(gameObject));
    }

    public void SwitchTree() => treePaused = !treePaused;

}
