using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// maintains a calculation of the distance between the nodes this edge is connected to, as a weight provision //
public class DistanceWeightUpdating : MonoBehaviour
{
    // connection: this edge's line renderer //
    private LineRenderer lineRenderer;
    // at the start: set the connection to this edge's line renderer //
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // float: the calculated distance between the nodes this edge is connected to – defaults to -1 until the edge has been drawn //
    public float distance = -1;

    // if the edge has been drawn: update the distance calculation //
    void Update()
    {
        if (GetComponent<ConnectedNodes>().drawnYet())        // if the edge has been drawn
        {
            distance = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        }
	}
}
