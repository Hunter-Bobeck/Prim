using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script is not in use, as it isn't currently working as intended */

// scales (locally) the graph, as long as both controllers have a grip button pressed, based on the movement of both controllers during those conditions* //
// for only one of the controllers, so that the scaling is not repeated //
public class GraphScaler : MonoBehaviour
{
    // connection to the other controller //
    private Controller otherController;
    void Start()
    {
        otherController = GetComponent<Controller>().other_controller.GetComponent<Controller>();
    }

    public Transform graphTransform;        // connection: graph - transform

    public float scaleMin, scaleMax;        // floats: min and max scale values

    // tracking of the starting (local) scale (rezeroed whenever scaling stops) //
    private Vector3 startingScale = Vector3.zero;
    // tracking of both controllers' last starting positions //
    private Vector3 lastStartingPosition, lastStartingPositionOther;

    // checking of conditions* and scaling (locally) based on the movement of both controllers when the conditions are met //
	void Update()
    {
        if (GetComponent<Controller>().grip_pressed() && otherController.grip_pressed())       // if both controllers' have a grip button pressed
        {
            // if the starting scale is zeroes, it needs to be set to the current scale of the graph so do so, and set the starting position of both controllers now //
            if (startingScale == Vector3.zero)
            {
                startingScale = graphTransform.localScale;

                lastStartingPosition = transform.position;
                lastStartingPositionOther = otherController.transform.position;
            }
            // otherwise (when the starting scale is already tracked): calculate the directionless difference between: the controllers' starting positions' distance, the controllers' current positions' distance; adjust the graph's (local) scale to be multiplied twice by this difference //
            float startingPositionsDistance = Vector3.Distance(lastStartingPosition, lastStartingPositionOther);
            float currentPositionsDistance = Vector3.Distance(transform.position, graphTransform.position);
            float distanceToScaleBy = currentPositionsDistance - startingPositionsDistance;
            float scaleFactor = 1;
            if (distanceToScaleBy > 0)
            {
                scaleFactor = Mathf.Lerp(1, 1.5f, distanceToScaleBy);
            }
            else if (distanceToScaleBy < 0)
            {
                scaleFactor = Mathf.Lerp(1, .5f, -distanceToScaleBy);
            }
            graphTransform.localScale = startingScale * scaleFactor * scaleFactor;/*new Vector3(Mathf.Clamp(startingScale.x * scaleFactor * scaleFactor, scaleMin, scaleMax), Mathf.Clamp(startingScale.y * scaleFactor * scaleFactor, scaleMin, scaleMax), Mathf.Clamp(startingScale.z * scaleFactor * scaleFactor, scaleMin, scaleMax));*/
        }
        // otherwise (if it's not the case that both controllers' have a grip button pressed), then if the tracked starting scale is not zeroes (and thus the grip button was just pressed): untrack the starting scale by setting it back to zeroes //
        else if (startingScale != Vector3.zero)
        {
            // set the starting scale back to zeroes //
            startingScale = Vector3.zero;
        }
    }
}