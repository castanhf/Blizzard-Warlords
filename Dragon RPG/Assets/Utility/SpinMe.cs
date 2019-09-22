using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;
	
	void Update () {

        // xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute
        // xDegreesPerFrame = seconds frame^-1 / seconds minute^-1 * degrees rotation^-1 * rotation minute^-1

        float xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute;
        // transform.right = x
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

        float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
        // transform.right = y
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
        // transform.right = z
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
