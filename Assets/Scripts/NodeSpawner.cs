using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns a node at this controller's position whenever the touchpad is pressing, so long as the controller is not currently colliding with a node //
public class NodeSpawner : MonoBehaviour
{
    public GameObject nodeTemplate;     // connection: node template

    public Transform nodesContainer;       // connection: nodes container - transform

    void Update()
    {
        if (GetComponent<Controller>().touchpad_pressing())     // if this controller's touchpad is pressing
        {
            if (NodeDetector.closestNodeInRange(transform.position) == null)      // if there is not a node within a node's radius of the controller's position
            {
                // spawn a node called "Node" at this controller's position, parented to the nodes container //
                GameObject node = Instantiate(nodeTemplate) as GameObject;
                node.name = "Node";
                node.transform.parent = nodesContainer;
                node.transform.position = transform.position;
            }
            // otherwise, /*log that there is already a node in this node radius*/ //
            else      // (there is a node within a node's radius of the controller's position) //
            {
                /*Debug.Log("there is already a node in this node radius");*/
            }
        }
    }
}