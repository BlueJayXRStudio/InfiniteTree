using System.Collections;
using System.Collections.Generic;
using InfiniteTree;
using UnityEngine;

public class EMSDriver : MonoBehaviour
{
    TaskStackMachine tree;

    void Awake()
    {
        tree = new(gameObject);
    }

    void Start()
    {
        tree.AddBehavior(new EMSControlFlow(gameObject));
    }

    void Update()
    {
        tree.Drive();
    }
}
