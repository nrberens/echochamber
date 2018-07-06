using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingSounds : MonoBehaviour {
/*
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
	public AudioClip pingClip;
	public float initPitch;
    
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        
        while (i < numCollisionEvents)
        {
			if(i%128==0) {
				AudioSource audio = gameObject.AddComponent<AudioSource>();
				audio.clip = pingClip;
				audio.pitch = initPitch * collisionEvents[i].normal.x;
				audio.Play();
				i++;
			}
        }
    }*/
}
