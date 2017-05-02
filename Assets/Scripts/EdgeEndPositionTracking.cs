using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// whenever this node is being held: updates the end positions by which this node's connected edges are connected //
public class EdgeEndPositionTracking : MonoBehaviour
{
    // tracking: list of integers representing the nearest end indeces (indeces by which the respective edge is attached to this node) parallel to the connected edges of this node //
    private List<int> nearestEndIndeces;
    // at the start: initialize the list of nearest end indeces to be an empty list //
    void Start()
    {
        nearestEndIndeces = new List<int>();
    }

	void Update()
    {
        if (GetComponent<FixedJoint>() != null)     // if this node is currently being held
        {
            List<GameObject> connectedEdges = GetComponent<ConnectedEdges>().connectedEdges;        // get a connection to the list of edges connected to this node
            
            // determine the list of nearest end indeces for this holding, if it is still empty //
            if (nearestEndIndeces.Count == 0)      // if the list of nearest end indeces has not yet been determined
            {
                // determine the nearest end index for each edge connected to this node, based on this node's position //
                foreach (GameObject connectedEdge in connectedEdges)
                {
                    Vector3 connectedEdgeEndFirst = connectedEdge.GetComponent<LineRenderer>().GetPosition(0);      // get the position of the first end for this edge
                    Vector3 connectedEdgeEndSecond = connectedEdge.GetComponent<LineRenderer>().GetPosition(1);      // get the position of the second end for this edge
                    // pick the index of the edge with the smaller distance to this node //
                    nearestEndIndeces.Add(((Vector3.Distance(transform.position, connectedEdgeEndFirst) <= Vector3.Distance(transform.position, connectedEdgeEndSecond)) ? 0 : 1));
                }
            }
            // update each connected edge's end position for this node (given by the parallel nearest end index that has been determined) to be at this node's current (moving) position //
            for (int connectedEdgeIndex = 0; connectedEdgeIndex < connectedEdges.Count; connectedEdgeIndex++)
            {
                connectedEdges[connectedEdgeIndex].GetComponent<LineRenderer>().SetPosition(nearestEndIndeces[connectedEdgeIndex], transform.position);
            }
        }
        else if (GetComponent<FixedJoint>() == null)        // otherwise, if this node is not being held...
        {
            // ...then if the nearest end indeces list is not empty, then this node must have recently been held and so the list still needs to be reset to be empty again, so do so //
            if (nearestEndIndeces.Count > 0)
            {
                nearestEndIndeces = new List<int>();
            }
        }
	}
}
