using UnityEngine;
using System.Collections;

// updates the collider box to be stretched from one end of its (parent) edge to the other //
public class ColliderBoxUpdating : MonoBehaviour
{
    // connection to the (parent) edge's line renderer; connected at the start //
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = transform.parent.GetComponent<LineRenderer>();
    }

    // tracked positions for the first and the second end position of the edge //
    private Vector3 edgeEndPositionFirst, edgeEndPositionSecond;

    void Update()
    {
        // update the tracked end positions of the edge //
        edgeEndPositionFirst = lineRenderer.GetPosition(0);
        edgeEndPositionSecond = lineRenderer.GetPosition(1);
        
        // center the collider box between the two end positions //
        transform.position = Vector3.Lerp(edgeEndPositionFirst, edgeEndPositionSecond, 0.5f);
        // have the collider box look at the second end position //
        transform.LookAt(edgeEndPositionSecond);
        // scale the collider box to be as long in the z-axis as the distance between the two end positions, minus the radius of either end's node – or to just (arbitrarily more than zero) be the radius of a node if it would otherwise be anything less //
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Max(.1f, (Vector3.Distance(edgeEndPositionFirst, edgeEndPositionSecond) - (.1f * 2))));
    }

    // rotation locking //
    /*Quaternion original_rotation;
    void Awake()
    {
        original_rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = original_rotation;
       
	// following x and z position of headset; placing y position centered between headset and floor //
        transform.position = new Vector3(headset.transform.position.x, ((headset.transform.position.y + floor.transform.position.y) / 2f), headset.transform.position.z);
    }*/
}
