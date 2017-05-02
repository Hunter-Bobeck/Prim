using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphResetter : MonoBehaviour
{
    public Controller controllerLeft, controllerRight;      // connections for both controller scripts

    // container transform connections: nodes, edges; set at the start
    private Transform containerNodes, containerEdges;
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Nodes")
                containerNodes = child;
            else if (child.name == "Edges")
                containerEdges = child;
        }
    }

    void Update()
    {
        if (controllerLeft.grip_pressed() && controllerRight.grip_pressed())        // when a grip button is pressed on both controllers
        {
            // erase all children of the nodes container //
            for (int i = containerNodes.childCount - 1; i >= 0; i--)
            {
                Destroy(containerNodes.GetChild(i).gameObject);
            }
            // erase all children of the edges container //
            for (int i = containerEdges.childCount - 1; i >= 0; i--)
            {
                Destroy(containerEdges.GetChild(i).gameObject);
            }
        }
    }
}