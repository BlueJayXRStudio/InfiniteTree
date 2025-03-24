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
            Debug.Log($"Position: ({(int) child.position.x}, {(int) child.position.z}) Material: {renderer.sharedMaterial.ToString().Split(' ')[0]}");
        }
    }

    void Update()
    {
        
    }
}
