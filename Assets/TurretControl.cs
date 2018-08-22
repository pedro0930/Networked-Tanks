using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurretControl : MonoBehaviour {
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	// Use this for initialization
	void Start () {
		
	}

	void Awake ()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask ("Ground");

        // Set up references.
        playerRigidbody = GetComponent <Rigidbody> ();
    }

	void FixedUpdate () 
	{
		Aim();

	}
	void Aim()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		 if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation (newRotation);
        }
	}
}
