using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// updates this object to face the player camera //
public class FacePlayerCamera : MonoBehaviour
{
    // connection – connected to at the start: (player) camera - transform //
    private Transform cameraTransform;
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // update the object's rotation to face the camera //
        transform.LookAt(cameraTransform);
    }
}