using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public enum AIState {
		Seek,
		Roam,
		Attack,	
	}

	public AIState currentAIState;
	public Vector3 lastKnownPosition;
	[SerializeField]
	private float attackDistance;
	[SerializeField]
	private float currentSeekTime;
	[SerializeField]
	private float maxSeekTime;
	[SerializeField]
	private float currentAttackTime;
	[SerializeField]
	private float maxAttackTime;
	[SerializeField]
	private Vector3 destination;
	
	private int currentWaypointIndex;
	[SerializeField]
	private Waypoint[] waypoints;
	[SerializeField]
	private float stoppingDistance;
	[SerializeField]
	private float roamSpeed;
	[SerializeField]
	private float seekSpeed;
	[SerializeField]
	private float attackSpeed;
	private Rigidbody rb;
	private MeshRenderer mesh;
	private TrailRenderer trail;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		mesh = GetComponent<MeshRenderer>();
		trail = GetComponent<TrailRenderer>();
		waypoints = GetComponentsInChildren<Waypoint>();
		AssignAndDetachWaypoints();
		currentAIState = AIState.Roam;
		currentWaypointIndex = 0;
		destination = GetWaypointPosition(currentWaypointIndex);
	}
	
	// Update is called once per frame
	void Update () {
		DetermineVisibility();
		
		switch(currentAIState) {
			case AIState.Roam:
				UpdateRoamState();
				break;
			case AIState.Seek:
				UpdateSeekState();
				break;
			case AIState.Attack:
				UpdateAttackState();
				break;
			default:
				break;
		}
	}

	private void UpdateRoamState() {

		if(destination == null) {
			currentWaypointIndex = GetNextWaypointIndex();
			destination = GetWaypointPosition(currentWaypointIndex);
			return;
		}

		if(Vector3.Distance(transform.position, destination) <= stoppingDistance) {
			currentWaypointIndex = GetNextWaypointIndex();
			destination = GetWaypointPosition(currentWaypointIndex);
			return;
		}

		//Otherwise continue toward destination
		Vector3 direction = (destination - transform.position).normalized * roamSpeed;
		rb.AddForce(direction);
		
		// if(destination == null) {
		// 	destination = FindRandomValidLocation(4.0f);
		// }
		// if(Vector3.Distance(transform.position, destination) <= stoppingDistance) {
		// 	destination = FindRandomValidLocation(4.0f);
		// }
	}

	private void UpdateSeekState() {
		if(destination == null) {
			currentAIState = AIState.Roam;
		} 

		currentSeekTime += Time.deltaTime;

		if(currentSeekTime >= maxSeekTime) {
			currentSeekTime = 0f;
			currentAIState = AIState.Roam;
			destination = GetWaypointPosition(currentWaypointIndex);
			return;
		}

		if(Vector3.Distance(transform.position, destination) <= attackDistance) {
			//ATTACK
			currentAIState = AIState.Attack;
		} else {
			//Otherwise continue toward destination
			Vector3 direction = (destination - transform.position).normalized * seekSpeed;
			rb.AddForce(direction);
		}
	}

	private void UpdateAttackState() {
		currentAttackTime += Time.deltaTime;

		if(currentAttackTime >= maxAttackTime) {
			currentAttackTime = 0f;
			Alert(GameController.gc.player.transform.position);
		}

		if(Vector3.Distance(transform.position, destination) <= attackDistance) {
			if(NPCUtility.HasLOSToTarget(transform, GameController.gc.player.transform)) {
				Vector3 direction = (destination - transform.position).normalized * attackSpeed;
				rb.AddForce(direction);
			}
		} else {
			currentAIState = AIState.Seek;
		}
	}

	public void Alert(Vector3 playerPosition) {
		lastKnownPosition = playerPosition;
		destination = lastKnownPosition;
		currentAIState = AIState.Seek;

	}

	private void AssignAndDetachWaypoints() {
		int index = 0;
		foreach(Waypoint w in waypoints) {
			w.owner = gameObject;
			w.index = index;
			w.transform.parent = null;
			index++;
		}
	}

	private Vector3 GetWaypointPosition(int index) {
		return waypoints[index].transform.position;
	}

	private int GetNextWaypointIndex() {
		int nextIndex = currentWaypointIndex + 1;
		if(nextIndex > waypoints.Length-1) nextIndex = 0;

		return nextIndex;
	}

	public void DetermineVisibility() {
		if(currentAIState == AIState.Roam) {
			TurnOffRenderers();
		} else {
			TurnOnRenderers();
		}
	}

	public void TurnOffRenderers() {
		if(mesh.enabled) {
			mesh.enabled = false;
		}

		if(trail.enabled) {
			trail.enabled = false;
		}
	}

	public void TurnOnRenderers() {
		if(!mesh.enabled) {
			mesh.enabled = true;
		}

		if(!trail.enabled) {
			trail.enabled = true;
		}
	}

	// private Vector3 FindRandomValidLocation(float range) {
	// 	Vector3 point;
    //     bool pointIsValid = false;
    //     do
    //     {
    //         pointIsValid = RandomPoint(transform.position, range, out point);
    //     } while (!pointIsValid);

    //     return point;
	// }

	// public static bool RandomPoint(Vector3 center, float range, out Vector3 result)
    // {
    //     for (int i = 0; i < 30; i++)
    //     {
    //         Vector3 randomPoint = center + Random.insideUnitSphere * range;
	// 		Vector3 elevatedPoint = randomPoint + (Vector3.up*0.5f);

	// 		if(!Physics.CheckSphere(elevatedPoint, 1.0f)) {
	// 			result = elevatedPoint;
	// 			return true;
	// 		}
    //     }
    //     result = Vector3.zero;
    //     return false;
	// }
    
}
