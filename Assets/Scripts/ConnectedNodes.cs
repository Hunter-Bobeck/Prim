using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedNodes : MonoBehaviour
{
    public List<GameObject> connectedNodes;     // tracking: list of connected nodes (the two nodes providing the points for this edge)
    // start: initialize 'connectedNodes' to be an empty list //
    void Start()
    {
        connectedNodes = new List<GameObject>();
    }

    // method: add a connection to the specified node to the list of connected nodes
    public void connectNode(GameObject node)
    {
        connectedNodes.Add(node);
    }

    // method: return a boolean representing whether given list of (to be connected) nodes of some pregiven edge has the same elements as this edge's list of connected nodes //
    public bool sameConnections(List<GameObject> nodes)
    {
        // setup a list of booleans parallel to the given list of nodes, with each boolean – defaulted to false – representing whether the parallel node has been found in this edge's list of connected nodes //
        List<bool> nodesFound = new List<bool>();
        foreach (GameObject node in nodes)
        {
            nodesFound.Add(false);
        }

        // figure out for each node in the given list of nodes whether it is present in this edge's list of connected nodes //
        for (int nodeIndex = 0; nodeIndex < nodes.Count; nodeIndex++)
        {
            GameObject node = nodes[nodeIndex];      // get a connection to the node at the current node index in the given list of nodes

            // for each connected node, if the current given node is the same node, then mark that node as found and move on to the next node index //
            foreach (GameObject connectedNode in connectedNodes)
            {
                if (connectedNode == node)
                {
                    nodesFound[nodeIndex] = true;
                    break;
                }
            }
        }

        // if one of the nodes was not found, return false; otherwise (if none of the nodes were not found, thus all of them were found), return true //
        foreach (bool nodeFound in nodesFound)
        {
            if (!nodeFound)
                return false;
        }
        return true;
    }

    // method: return a boolean for whether this edge has been drawn yet (based on whether it has two connected nodes yet) //
    public bool drawnYet()
    {
        return (connectedNodes.Count == 2);
    }
}