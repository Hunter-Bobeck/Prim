using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedEdges : MonoBehaviour
{
    public List<GameObject> connectedEdges;     // tracking: list of connected edges (edges with one point at this node's position)
    // start: initialize 'connectedEdges' to be an empty list //
    void Start()
    {
        connectedEdges = new List<GameObject>();
    }

    // method: add a connection to the specified edge to the list of connected edges
    public void connectEdge(GameObject edge)
    {
        connectedEdges.Add(edge);
    }

    // method: determine if the given edge is identical to an edge already connected to this node //
    public bool identicalEdge(List<GameObject> nodes)
    {
        // if one of this node's connected edges is identical to the given edge, return true //
        foreach (GameObject connectedEdge in connectedEdges)
        {
            if (connectedEdge.GetComponent<ConnectedNodes>().sameConnections(nodes))
            {
                return true;
            }
        }
        // otherwise, return false
        return false;
    }
}