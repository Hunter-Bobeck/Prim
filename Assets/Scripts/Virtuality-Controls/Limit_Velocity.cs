using UnityEngine;
using System.Collections;

public class Limit_Velocity : MonoBehaviour
{
	// connection to the object's rigidbody //
	Rigidbody object_rigidbody;
	void Start()
	{
		object_rigidbody = GetComponent<Rigidbody>();
	}
	// max velocity to limit to //
	public float max_velocity;
	// limiting of velocity to max velocity //
	void FixedUpdate()
	{
		object_rigidbody.velocity = Vector3.ClampMagnitude(object_rigidbody.velocity, max_velocity);
	}
}
