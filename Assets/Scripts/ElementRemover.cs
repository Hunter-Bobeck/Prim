using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// removes any node colliding with this controller when a grip button is pressed, updating connections accordingly //
public class ElementRemover : MonoBehaviour
{
	void OnTriggerStay(Collider collider)
    {
        if (GetComponent<Controller>().grip_pressed())      // if a grip is pressed on this controller
        {
            if (collider.gameObject.tag == "Node")      // if the collided object is a node
            {
                GameObject node = collider.gameObject;       // get a connection to the collided node

                // remove any other nodes' connections to any of the node's connected edges, and then any of the node's connected edges themselves //
                List<GameObject> connectedEdges = node.GetComponent<ConnectedEdges>().connectedEdges;
                foreach (GameObject connectedEdge in connectedEdges)
                {
                    // remove any other nodes' connections to any of the node's connected edges //
                    List<GameObject> connectedNodes = connectedEdge.GetComponent<ConnectedNodes>().connectedNodes;
                    foreach (GameObject connectedNode in connectedNodes)
                    {
                        if (connectedNode != node)
                        {
                            List<GameObject> connectedNodeConnectedEdges = connectedNode.GetComponent<ConnectedEdges>().connectedEdges;
                            for (int i = connectedNodeConnectedEdges.Count - 1; i >= 0; i--)
                            {
                                if (connectedNodeConnectedEdges[i] == connectedEdge)
                                    connectedNodeConnectedEdges.RemoveAt(i);
                            }
                        }
                    }
                    // remove any of the node's connected edges //
                    Destroy(connectedEdge);
                }
                // destroy the node //
                Destroy(node);
            }
            /*else if (collider.gameObject.tag == "Edge Collider Box")      // otherwise, if the collided object is an edge collider box
            {
                GameObject edge = collider.gameObject.transform.parent.gameObject;       // get a connection to the collided edge collider box's (parent) edge

                // for each of the nodes this edge is connected to, remove the connections to this edge //
                List<GameObject> connectedNodes = edge.GetComponent<ConnectedNodes>().connectedNodes;
                foreach (GameObject connectedNode in connectedNodes)
                {
                    List<GameObject> connectedNodeConnectedEdges = connectedNode.GetComponent<ConnectedEdges>().connectedEdges;
                    for (int i = connectedNodeConnectedEdges.Count - 1; i >= 0; i--)
                    {
                        if (connectedNodeConnectedEdges[i] == edge)
                            connectedNodeConnectedEdges.RemoveAt(i);
                    }
                }
                // destroy the edge //
                Destroy(edge);
            }*/
        }
	}
}