using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetector : MonoBehaviour
{
    // method: return the closest node found (or null, if none) within the range of a node's radius to the given position //
    public static GameObject closestNodeInRange(Vector3 givenPosition)
    {
        Collider[] potentialNodeColliders = Physics.OverlapSphere(givenPosition, .1f);        // get an array of the colliders found in the range of a node's radius to the given position
        List<GameObject> nodesInRange = new List<GameObject>();     // setup a list for nodes found in range
        foreach (Collider collider in potentialNodeColliders)       // for each collider in the array
        {
            // if the collider's object is a node, then add the node to the list of nodes found in range //
            if (collider.gameObject.tag == "Node")
                nodesInRange.Add(collider.gameObject);
        }

        // go through the list of nodes found in range and return the one with the shortest distance to the given position, if* there are any nodes in the list //
        float shortestDistance = float.MaxValue;        // tracking: shortest distance out of any of the potential nodes found in range
        int shortestDistanceNodeIndex = -1;
        for (int nodeIndex = 0; nodeIndex < nodesInRange.Count; nodeIndex++)
        {
            GameObject node = nodesInRange[nodeIndex];        // get a connection to the node at the current node index in 'nodesInRange'

            float distance = Vector3.Distance(node.transform.position, givenPosition);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                shortestDistanceNodeIndex = nodeIndex;
            }
        }

        if (shortestDistanceNodeIndex != -1)       // if the shortest distance node index was set past its default of -1: returning the the node with the shortest distance (given by that index)
            return nodesInRange[shortestDistanceNodeIndex];
        // otherwise* (if the shortest distance node index was not set past its default of -1, thus the list of nodes found in range was empty), return null //
        return null;
    }
}