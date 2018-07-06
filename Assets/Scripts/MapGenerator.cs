using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public int mapWidth, mapHeight, roomMaxSize, roomMinSize, maxRooms;
	public Transform floorTile, wallTile;

	public GameObject[,] map;

	// Use this for initialization
	void Start () {

		map = new GameObject[mapWidth, mapHeight];

		for(int j = 0; j < mapHeight; j++) {
			for(int i = 0; i < mapWidth; i++) {
				GameObject o = new GameObject();
				o.name = i + "," + j;
				o.transform.localPosition = new Vector3(i, j, 0);
				Tile tile = new Tile();
				o.AddComponent<Tile>();
				map[i,j] = o;
				map[i,j].transform.parent = GameObject.Find("Map").transform;
				map[i,j].GetComponent<Tile>().blocked = true;
			}
		}

		Rect[] rooms = new Rect[maxRooms];
		int numRooms = 0;

		for(int i = 0; i < maxRooms; i++) {
			int w = Random.Range(roomMinSize, roomMaxSize);
			int h = Random.Range(roomMinSize, roomMaxSize);
			int x = Random.Range(0, mapWidth - w - 1);
			int y = Random.Range(0, mapHeight - h - 1);
			
			Rect newRoom = new Rect(x,y,w,h);
			bool failed = false;

			foreach(Rect otherRoom in rooms) {
				failed = Intersect(newRoom, otherRoom);
				if(failed) {
					break;
					Debug.Log("Failed!");
				}
			}

			if(!failed) {
				CreateRoom(newRoom);

				Vector2 newRoomXY = newRoom.center;

				//Connect with tunnels
				if(numRooms == 0) {
					//GameObject.Find("Player").transform.position = new Vector3(newRoom.x, newRoom.y, 0f);
				} else {
					//Center coordinates of previous room
					Vector2 otherRoomXY = rooms[numRooms-1].center;

					//flip a coin: horizontal or vertical tunnel first?
					if (Random.Range(0, 1) == 1) {
						CreateHTunnel((int)otherRoomXY.x, (int)newRoomXY.x, (int)otherRoomXY.y);
						CreateVTunnel((int)otherRoomXY.y, (int)newRoomXY.y, (int)newRoomXY.x);
					}
					else {
						CreateVTunnel((int)otherRoomXY.y, (int)newRoomXY.y, (int)otherRoomXY.x);
						CreateHTunnel((int)otherRoomXY.x, (int)newRoomXY.x, (int)otherRoomXY.y);
					}
				}

				rooms[numRooms] = newRoom;
				numRooms++;
			}
		}

		InstantiateMap();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateRoom(Rect room) {
		//fill the room with unblocked tiles
		//walls = 1 tile thick
		for (int a = (int)room.x + 1; a < room.x + room.width; a++) {
			for (int b = (int)room.y + 1; b < room.y + room.height; b++) {
				map[a, b].GetComponent<Tile>().blocked = false;
			}
		}
	}

	private void CreateHTunnel(int x1, int x2, int y) {
		//horizontal tunnel
		//Mathf.Min and Mathf.Max are used in case x2 > x1
		for (int x = Mathf.Min(x1, x2); x < Mathf.Max(x1, x2); x++){
			map[x, y].GetComponent<Tile>().blocked = false;
		}
	}

	private void CreateVTunnel(int y1, int y2, int x) {
		//vertical tunnel
		for (int y = Mathf.Min(y1, y2); y < Mathf.Max(y1, y2); y++) {
			map[x, y].GetComponent<Tile>().blocked = false;
		}
	}

	private bool Intersect(Rect room1, Rect room2) {
		 //check if two rooms intersect
		if (room1.x <= (room2.x + room2.width) && (room1.x + room1.width) >= room2.x &&
				room1.y <= (room2.y + room2.height) && (room1.height + room1.y) >= room2.y) {
			return true;
		}
		else {
			return false;
		}
	}

	private void InstantiateMap() {

		for (int y = 0; y < mapHeight; y++){
			for (int x = 0; x < mapWidth; x++) {
				GameObject go = map[x,y];
				if (go.GetComponent<Tile>().blocked){
					go.GetComponent<Tile>().PlaceTile(go, wallTile, new Vector3(x, y, 0));
				}
				else if (go.GetComponent<Tile>().blocked == false) {
					go.GetComponent<Tile>().PlaceTile(go, floorTile, new Vector3(x, y, -0.5f));
				}
			}
		}
	}
}