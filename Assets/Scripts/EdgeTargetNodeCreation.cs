using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTargetNodeCreation : MonoBehaviour
{
    // connection: connected nodes //
    private ConnectedNodes connectedNodes;
    void Start()
    {
        connectedNodes = GetComponent<ConnectedNodes>();
    }

    public void spawnNode(NodeSpawner spawningController)
    {
        if (connectedNodes.connectedNodes.Count == 1)
        {
            GameObject targetNode = spawningController.GetComponent<NodeSpawner>().spawnNode();
            connectedNodes.connectNode(targetNode);

            targetNode.GetComponent<ConnectedEdges>().connectEdge(gameObject);
            GetComponent<ConnectedNodes>().connectNode(targetNode);
        }
    }
}
