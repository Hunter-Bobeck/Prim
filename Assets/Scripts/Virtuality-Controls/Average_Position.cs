using UnityEngine;
using System.Collections;

public class Average_Position : MonoBehaviour
{
	// transforms to average positions of //
	public Transform[] transforms;
	// setting position to the average of the positions in the transforms //
	void Update()
	{
		Vector3 average_position = Vector3.zero;
		foreach (Transform transform_to_average in transforms)
		{
			average_position += transform_to_average.position;
		}
		average_position /= transforms.Length;
		transform.position = average_position;
	}
}
