using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentComponent : MonoBehaviour
{
    private int WP = 0;
    private List<GameObject> Waypoints;

    public void SetParents(List<GameObject> waypoints) => Waypoints = waypoints;
    public GameObject GetParent => Waypoints[WP];
    public void iterate() => WP = (WP + 1) % Waypoints.Count;
}
