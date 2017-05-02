using UnityEngine;
using System.Collections;

public class Delayed_Destruction : MonoBehaviour
{
	public float delay;
	void Start()
	{
		Invoke("destroy", delay);
	}
	void destroy()
	{
		Destroy(gameObject);
	}
}
