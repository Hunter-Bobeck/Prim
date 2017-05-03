using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// whenever either of the controllers' menu buttons are pressing: toggles the 'MST' (Prim minimum spanning tree) visualization for the graph //
public class AlgorithmController : MonoBehaviour
{
    public Controller controllerLeft, controllerRight;      // connections for both controller scripts

    public Material unhighlighted, highlighted;        // connections to edge materials: unhighlighted, highlighted

    private Transform containerNodes, containerEdges;       // container transform connections: nodes, edges; set at the start*

    private GameObject startingNode;     // tracks the chosen starting node for the current visualization; initialized to null at the start*

    // *starting setup //
    void Start()
    {
        // container transform connections: nodes, edges //
        foreach (Transform child in transform)
        {
            if (child.name == "Nodes")
                containerNodes = child;
            else if (child.name == "Edges")
                containerEdges = child;
        }

        // starting node initialization //
        startingNode = null;
    }

    // method: determine if the MST is currently displaying //
    public bool displaying()
    {
        return (startingNode != null);      // if the starting node is not null: the MST is displaying; otherwise, if the starting node is null: the MST is not displaying
    }

    void Update()
    {
        if (containerNodes.childCount > 0)
        {
            if (((controllerLeft != null) && controllerLeft.menu_button_pressing()) || ((controllerRight != null) && controllerRight.menu_button_pressing()))        // when either of the controllers' menu buttons is pressing
            {
                // if the MST is not displaying: begin displaying it
                if (!displaying())
                {
                    // track that the MST is now displaying; determine the starting node //
                    Controller pressingController = (controllerLeft.menu_button_pressing() ? controllerLeft : controllerRight);        // determine which controller is doing the pressing and thus to choose the nearest node from – choose the left controller if both are
                    startingNode = GetComponent<NodeDetector>().nodeNearestTo(pressingController.transform.position);       // choose the node closest to the pressing controller

                    // begin displaying the MST //
                    display();
                }

                // otherwise (if the MST is displaying): stop displaying it
                else
                {
                    // reset the tracked starting node to null //
                    startingNode = null;

                    // stop displaying the MST //
                    foreach (Transform edgeTransform in containerEdges)
                    {
                        edgeTransform.GetComponent<LineRenderer>().material = unhighlighted;
                    }
                }
            }
        }
    }

    // method: begin displaying the MST //
    void display()
    {
        List<GameObject> connectedEdges = startingNode.GetComponent<ConnectedEdges>().connectedEdges;
        foreach (GameObject connectedEdge in connectedEdges)
        {
            connectedEdge.GetComponent<LineRenderer>().material = highlighted;
        }
    }
}