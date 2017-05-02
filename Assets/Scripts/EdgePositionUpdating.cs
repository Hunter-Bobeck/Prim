using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// updates the edge's position to be the midpoint between its end positions //
public class EdgePositionUpdating : MonoBehaviour
{
    // connection: line renderer; set at the start //
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        // update the edge's position to be the midpoint between its end positions //
        transform.position = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), .5f);
    }
}