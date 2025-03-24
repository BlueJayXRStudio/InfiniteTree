using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTool : MonoBehaviour
{
    void Start()
    {
        var tileAnchor = GameObject.Find("Tiles");

        for (int i = 0; i < tileAnchor.transform.childCount; i++) {
            var child = tileAnchor.transform.GetChild(i);
            var renderer = child.GetComponent<MeshRenderer>();
            // Debug.Log($"Position: ({Mathf.RoundToInt(child.position.x)}, {Mathf.RoundToInt(child.position.z)}) Material: {renderer.sharedMaterial.ToString().Split(' ')[0]}");
            ExperimentBlackboard.Instance.map.Add(
                (Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.z)), 
                renderer.sharedMaterial.ToString().Split(' ')[0] == "m_Brown" ? "wall" : "ground"
            );
        }

        Debug.Log(ExperimentBlackboard.Instance.map.Count);
        // var keys = new List<(int, int)>(ExperimentBlackboard.Instance.map.Keys);
        // keys.Sort();
        // foreach ((int, int) key in keys) {
        //     Debug.Log(key);
        // }
    }

    void Update()
    {
        
    }
}
