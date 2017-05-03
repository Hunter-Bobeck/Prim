using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles drawing of edges from a starting node to a target node; creates a new node when no target node is found //
public class EdgeDrawer : MonoBehaviour
{
    public GameObject nodeTemplate;      // connection: node template
    public GameObject edgeTemplate;     // connection: edge template

    private GameObject edgeBeingDrawn;       // tracking: edge being currently drawn (if null, then none is)
    private GameObject startingNode;        // tracking: starting node for the edge being currently drawn (if null, then no edge is currently being drawn so there is no starting node to track)

    public Transform edgesContainer;       // connection: edges container - transform
    public Transform nodesContainer;       // connection: nodes container - transform

    void Update()
    {
        // edge drawing: starting an edge //
        GameObject potentialNodeInRange = NodeDetector.closestNodeInRange(transform.position);     // connect to a potential closest node in range of the controller's position
        if ((edgeBeingDrawn == null) && GetComponent<Controller>().touchpad_pressing() && (potentialNodeInRange != null))     // if: no edge is currently being drawn, this controller's touchpad is pressing, there is a node within a node's radius of the controller's position
        {
            // spawn an edge called "Edge", parented to the edges container //
            GameObject edge = Instantiate(edgeTemplate) as GameObject;
            edge.name = "Edge";
            edge.transform.parent = edgesContainer;
            // set the edge's first point to be the position of the closest node in range of the controller's position //
            edge.GetComponent<LineRenderer>().SetPosition(0, potentialNodeInRange.transform.position);
            // set the edge's second point to be the controller's position //
            edge.GetComponent<LineRenderer>().SetPosition(1, transform.position);
            // set the edge's position to be the midpoint between its ends' (deriving) positions //
            edge.transform.position = Vector3.Lerp(potentialNodeInRange.transform.position, transform.position, .5f);
            // track the edge being drawn //
            edgeBeingDrawn = edge;
            // track the starting node for the edge being drawn //
            startingNode = potentialNodeInRange;
        }
        // edge drawing: continuing an edge without a target node found //
        else if ((edgeBeingDrawn != null) && GetComponent<Controller>().touchpad_pressed() && ((potentialNodeInRange == null) || (potentialNodeInRange == startingNode)))       // if: an edge is currently being drawn, this controller's touchpad is pressed, there is no node other than the starting node within range
        {
            // set the edge's second point to be controller's position //
            edgeBeingDrawn.GetComponent<LineRenderer>().SetPosition(1, transform.position);
            // set the edge's position to be the midpoint between its ends' (deriving) positions //
            edgeBeingDrawn.transform.position = Vector3.Lerp(startingNode.transform.position, transform.position, .5f);
        }
        // edge drawing: canceling an edge – no target node found, so create one //
        else if ((edgeBeingDrawn != null) && GetComponent<Controller>().touchpad_unpressing() && ((potentialNodeInRange == null) || (potentialNodeInRange == startingNode)))        // if: an edge is currently being drawn, this controller's touchpad is unpressing, there is no node other than the starting node within range
        {
            // spawn a node called "Node" at this controller's position, parented to the nodes container //
            GameObject node = Instantiate(nodeTemplate) as GameObject;
            node.name = "Node";
            node.transform.parent = nodesContainer;
            node.transform.position = transform.position;

            // finish this edge drawing //
            // set the edge's second point to be the target node's position //
            edgeBeingDrawn.GetComponent<LineRenderer>().SetPosition(1, node.transform.position);
            // set the edge's position to be the midpoint between its ends' (deriving) positions //
            edgeBeingDrawn.transform.position = Vector3.Lerp(startingNode.transform.position, node.transform.position, .5f);
            // track the edge drawn as connected for both the starting and the finishing nodes //
            startingNode.GetComponent<ConnectedEdges>().connectEdge(edgeBeingDrawn);
            Debug.Log("about to do: node.GetComponent<ConnectedEdges>().connectEdge(edgeBeingDrawn);");
            node.GetComponent<ConnectedEdges>().connectEdge(edgeBeingDrawn);
            // track both the starting and finishing nodes as being connected to the edge drawn //
            edgeBeingDrawn.GetComponent<ConnectedNodes>().connectNode(startingNode);
            edgeBeingDrawn.GetComponent<ConnectedNodes>().connectNode(node);
            // reset both tracking variables to null //
            edgeBeingDrawn = null;
            startingNode = null;
        }
        // edge drawing: continuing an edge with a potential target node found //
        else if ((edgeBeingDrawn != null) && GetComponent<Controller>().touchpad_pressed() && ((potentialNodeInRange != null) && (potentialNodeInRange != startingNode)))       // if: an edge is currently being drawn, this controller's touchpad is pressed, there is a node other than the starting node within range
        {
            // set the edge's second point to be controller's position //
            edgeBeingDrawn.GetComponent<LineRenderer>().SetPosition(1, potentialNodeInRange.transform.position);
            // set the edge's position to be the midpoint between its ends' (deriving) positions //
            edgeBeingDrawn.transform.position = Vector3.Lerp(startingNode.transform.position, potentialNodeInRange.transform.position, .5f);
        }
        // edge drawing: target node found – finishing an edge \ canceling an edge (if the edge being drawn is identical to an edge connected to the target node) //
        else if ((edgeBeingDrawn != null) && GetComponent<Controller>().touchpad_unpressing() && ((potentialNodeInRange != null) && (potentialNodeInRange != startingNode)))      // if: an edge is currently drawn, this controller's touchpad is unpressing, there is a node other than the starting node within range
        {
            // if the target node found already has an identical edge: cancel this edge drawing //
            if (potentialNodeInRange.GetComponent<ConnectedEdges>().identicalEdge(new List<GameObject>(new GameObject[] { startingNode, potentialNodeInRange })))
            {
                // destroy the edge being drawn //
                Destroy(edgeBeingDrawn);
                // reset both tracking variables to null //
                edgeBeingDrawn = null;
                startingNode = null;
            }
            // otherwise, finish this edge drawing
            else
            {
                // set the edge's second point to be the target node's position //
                edgeBeingDrawn.GetComponent<LineRenderer>().SetPosition(1, potentialNodeInRange.transform.position);
                // set the edge's position to be the midpoint between its ends' (deriving) positions //
                edgeBeingDrawn.transform.position = Vector3.Lerp(startingNode.transform.position, potentialNodeInRange.transform.position, .5f);
                // track the edge drawn as connected for both the starting and the finishing nodes //
                startingNode.GetComponent<ConnectedEdges>().connectEdge(edgeBeingDrawn);
                potentialNodeInRange.GetComponent<ConnectedEdges>().connectEdge(edgeBeingDrawn);
                // track both the starting and finishing nodes as being connected to the edge drawn //
                edgeBeingDrawn.GetComponent<ConnectedNodes>().connectNode(startingNode);
                edgeBeingDrawn.GetComponent<ConnectedNodes>().connectNode(potentialNodeInRange);
                // reset both tracking variables to null //
                edgeBeingDrawn = null;
                startingNode = null;
            }
        }
    }
}