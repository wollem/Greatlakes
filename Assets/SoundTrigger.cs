using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {

	public AudioSource audio; 

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			audio.Play();
			Destroy(gameObject, audio.clip.length);
		}
	}

}
