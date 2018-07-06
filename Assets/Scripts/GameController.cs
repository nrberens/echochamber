using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController gc;
	public GameObject player;
	public int currentLevelNum;

	[SerializeField]

	// Use this for initialization
	void Awake () {
		if(gc == null) {
			DontDestroyOnLoad(gameObject);
			gc = this;
		} else if(gc != null) {
			Destroy(gameObject);
		}

		player = GameObject.FindGameObjectWithTag("Player");
		currentLevelNum = 1;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}

		if(Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void NextLevel() {
		currentLevelNum++;
		int tens = (int) (currentLevelNum%100) - (currentLevelNum%10);
		int ones = (int) (currentLevelNum%10);
		string nextMapName = "map" + tens + ones;
		SceneManager.LoadScene(nextMapName);
	}


}
