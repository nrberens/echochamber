using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	[SerializeField]
	private float speed;
	PlayerPing ping;
	Rigidbody rb;
	AudioSource asource;

	// Use this for initialization
	void Start () {
		ping = GetComponent<PlayerPing>();
		rb = GetComponent<Rigidbody>();
		asource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveZ = Input.GetAxis("Vertical");
		float moveX = Input.GetAxis("Horizontal");
		Vector3 moveDir = new Vector3(moveX*speed, 0f, moveZ*speed);

		rb.AddForce(moveDir);

		if(Input.GetButtonDown("Ping")) {
			asource.Play();
			ping.SendPing();
		}
		
	}
}
