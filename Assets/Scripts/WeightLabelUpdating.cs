using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // have to import this to know about the Text type of component

// updates this textual label part of the canvas to match the latest weight calculation //
public class WeightLabelUpdating : MonoBehaviour
{
    // connections – connected to at the start: text, distance weight updating //
    private Text text;
    private DistanceWeightUpdating distanceWeightUpdating;
    void Start()
    {
        text = GetComponent<Text>();
        distanceWeightUpdating = transform.parent.parent.GetComponent<DistanceWeightUpdating>();
    }

	void Update()
    {
        // update the label text to match the latest weight calculation //
        text.text = ""+distanceWeightUpdating.distance;
	}
}