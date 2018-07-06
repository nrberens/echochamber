using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPing : MonoBehaviour {
	[SerializeField]
	private GameObject ping;
	[SerializeField]
	private ParticleSystem particles;
	[SerializeField] ParticleSystem[] allParticles;
	[SerializeField]
	private int particleCount;
	[SerializeField]
	private float timeSinceLastPing;
	[SerializeField]
	private float timeBetweenPings;
	[SerializeField]
	private float radius;
	[SerializeField]
	private float alertRange;

	// Use this for initialization
	void Start () {
		ping = transform.Find("PulseFX/Pulse Collision").gameObject;
		allParticles = GetComponentsInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastPing += Time.deltaTime;	
	}

	public void SendPing() {
		if(timeSinceLastPing > timeBetweenPings) {
			timeSinceLastPing = 0f;
			EmitAllParticles();
			EmitSphere();
			AlertNearbyEnemies();
		}
	}

	public void EmitAllParticles() {
		foreach(ParticleSystem ps in allParticles) {
			ps.Play();
		}
	}

	public void EmitSphere() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].CompareTag("Wall")) {
				Wall wall = colliders[i].GetComponent<Wall>();
				Vector3 dir = wall.transform.position - transform.position;
				Vector3 dest = wall.transform.position + (dir.normalized/2);
				float distance = Vector3.Distance(wall.transform.position, transform.position);
				wall.Nudge(dest, distance);
				wall.FadeMat(distance);
			} else if (colliders[i].CompareTag("Floor")) {
				Floor floor = colliders[i].GetComponent<Floor>();
				Vector3 dir = floor.transform.position - transform.position;
				Vector3 dest = floor.transform.position + (dir.normalized/2);
				float distance = Vector3.Distance(floor.transform.position, transform.position);
				//floor.Nudge(dest, distance);
				floor.FadeMat(distance);
			}
		}
	}

	public void AlertNearbyEnemies() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, alertRange);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].CompareTag("Enemy")) {
				Enemy enemy = colliders[i].GetComponent<Enemy>();
				enemy.Alert(transform.position);
			}
		}
	}
}
