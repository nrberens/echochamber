using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtils : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Toggle Walls")) {
			ToggleWalls();
		}
	}

	void ToggleWalls() {
		Wall[] walls = FindObjectsOfType<Wall>();

		foreach(Wall w in walls) {
			MeshRenderer r = w.gameObject.GetComponent<MeshRenderer>();
			r.enabled = !r.enabled;
		}
	}
}
