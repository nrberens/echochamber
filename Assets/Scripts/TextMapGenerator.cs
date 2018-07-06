using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class TextMapGenerator : MonoBehaviour {
	public int height, width;
	StreamReader sr;
	char[,] textMap;

	public GameObject wallPrefab;
	public GameObject floorPrefab;
	public GameObject enemyPrefab;
	public GameObject exitPrefab;
	public Level level;

	// Use this for initialization
	void Start () {
		level = GetComponent<Level>();
		TextAsset file = Resources.Load(level.mapName) as TextAsset;
		textMap = new char[width, height];

		string fullText = file.text; 
		int index = 0;
		for (int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				char tile = fullText[index];
				while(tile != '#' && tile != '-' && tile != '@' && tile != 'X' && tile != '$') { 
					index++;
					tile = fullText[index];
				}

				textMap[x, y] = tile; 
				index++;
			}
		}

		InstantiateMap();
	}

	private void InstantiateMap() {
		for (int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				char tile = textMap[x,y];

				float scaledX = x * 0.5f;
				float scaledY = y * 0.5f;
				switch(tile) {
					case '#':
						PlaceWall(scaledX,scaledY);
						break;
					case '-':
						PlaceFloor(scaledX, scaledY);
						break;
					case '@':
						PlacePlayer(scaledX, scaledY);
						break;
					case 'X':
						PlaceEnemy(scaledX, scaledY);
						break;
					case '$':
						PlaceExit(scaledX, scaledY);
						break;
					default:
						Debug.Log(tile);
						break;
				}
			}
		}
	}

	private void PlaceWall(float x, float y) {
		PlaceFloor(x,y);
		Vector3 position = new Vector3(x, 0.5f, y);
		GameObject go = Instantiate(wallPrefab, position, Quaternion.identity);
		go.transform.parent = transform;
		go.name = x + "," + y + " Wall";
	}

	private void PlaceFloor(float x, float y) {
		Vector3 position = new Vector3(x, 0, y);
		GameObject go = Instantiate(floorPrefab, position, Quaternion.identity);
		go.transform.parent = transform;
		go.name = x + "," + y + " Floor";
	}

	private void PlacePlayer(float x, float y) {
		PlaceFloor(x,y);
		GameObject player = GameObject.Find("Player");
		player.transform.position = new Vector3(x, 0.5f, y);
	}

	private void PlaceEnemy(float x, float y) {
		PlaceFloor(x,y);
		Vector3 position = new Vector3(x, 0.5f, y);
		GameObject go = Instantiate(enemyPrefab, position, Quaternion.identity);
		go.name = "Enemy";
	}
	
	private void PlaceExit(float x, float y) {
		PlaceFloor(x,y);
		Vector3 position = new Vector3(x, 0.5f, y);
		GameObject go = Instantiate(exitPrefab, position, Quaternion.identity);
		go.name = "Exit";
	}
}
