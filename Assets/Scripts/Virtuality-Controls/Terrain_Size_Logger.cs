using UnityEngine;
using System.Collections;

public class Terrain_Size_Logger : MonoBehaviour
{
	void Start()
	{
		// determination of terrain size //
		Vector3 size = GetComponent<Terrain>().terrainData.size;
		// logging of terrain size //
		Debug.Log("x size: "+size.x+",\ny size: "+size.y+",\nz size: "+size.z);
	}
}
