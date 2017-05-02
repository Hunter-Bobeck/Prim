using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
	// direction possibilities definition //
	public enum Direction
	{
		random,
		forward,
		backward,
		right,
		left,
		up,
		down
	}
	// direction of rotation //
	public Direction direction;
	// vector for direction of rotation //
	Vector3 direction_vector;
	// determination of vector for direction //
	void Start()
	{
		direction_vector = Vector3.zero;
		switch (direction)
		{
			case Direction.random:
				direction_vector = Random.insideUnitSphere;
				break;
			case Direction.forward:
				direction_vector = Vector3.forward;
				break;
			case Direction.backward:
				direction_vector = -Vector3.forward;
				break;
			case Direction.right:
				direction_vector = Vector3.right;
				break;
			case Direction.left:
				direction_vector = -Vector3.right;
				break;
			case Direction.up:
				direction_vector = Vector3.up;
				break;
			case Direction.down:
				direction_vector = -Vector3.up;
				break;
		}
	}
	// rotation speed //
	public float speed;
	// rotation //
	void Update()
	{
		transform.Rotate(direction_vector * Time.deltaTime * speed);
	}
}
