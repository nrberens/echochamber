using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Player")) {
			Debug.Log("Player entered the exit!");
			ExitLevel();
		}
	}

	public void ExitLevel() {
		GameController.gc.NextLevel();
	}
}
