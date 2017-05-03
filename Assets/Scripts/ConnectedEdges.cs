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
        Debug.Log("Node "+GetInstanceID()+" is trying to list edge "+ edge.GetInstanceID()+" as connected.");
        connectedEdges.Add(edge);
        Debug.Log("First edge:"+connectedEdges[0].name);
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

    // method: determine if this node has an edge connecting it to the given node, and if so, return it; otherwise, return null //
    /* example usage to determine if a node 'u' has a connection to a node 'v': bool connected = (u.GetComponent<ConnectedEdges>().connectedTo(v) == null) */
    /* example usage to determine get the edge by which a node 'u' is connected to a node 'v': GameObject edge = u.GetComponent<ConnectedEdges>().connectedTo(v); */
    public GameObject connectedTo(GameObject otherNode)
    {
        foreach (GameObject connectedEdge in connectedEdges)
        {
            List<GameObject> connectedEdgeConnectedNodes = connectedEdge.GetComponent<ConnectedNodes>().connectedNodes;
            
            foreach (GameObject connectedEdgeConnectedNode in connectedEdgeConnectedNodes)
            {
                if (connectedEdgeConnectedNode == otherNode)
                    return connectedEdge;
            }
        }
        return null;
    }
}