using UnityEngine;
using System.Collections;

public class TurnOffZone : MonoBehaviour {

	public ParticleSystem particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			particle.emissionRate = 0f;
		}
	}
}
