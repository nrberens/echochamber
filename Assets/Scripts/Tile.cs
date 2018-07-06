
using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour {
public bool blocked;
public int x, y;


	public void PlaceTile(GameObject myParent, Transform prefab, Vector3 v) {
		Transform t = GameObject.Instantiate(prefab);
		t.localPosition = v;
		t.transform.parent = myParent.transform;
	}
}