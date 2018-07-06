using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			StartGame();
		}
	}

	public void StartGame() {
		SceneManager.LoadScene("map01");
	}
}
