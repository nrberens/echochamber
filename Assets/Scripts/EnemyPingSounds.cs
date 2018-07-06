using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPingSounds : MonoBehaviour {

	public AudioSource asource;
	public ParticleSystem ping;

	// Use this for initialization
	void Start () {
		asource = GetComponent<AudioSource>();
		ping = GetComponent<ParticleSystem>();
		StartCoroutine(RepeatPing());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator RepeatPing() {
		while(gameObject.active) {
			yield return new WaitForSeconds(ping.duration*2);
			asource.Play();
		}
	}
}
