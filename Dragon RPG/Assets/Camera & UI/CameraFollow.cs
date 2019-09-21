using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;       // Public variable to store a reference to the player game object

    private Vector3 offset;         // Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
