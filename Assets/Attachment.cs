using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour {

	public GameObject turret;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		turret.transform.position = transform.TransformPoint (0, 1, 0);
	}
}
