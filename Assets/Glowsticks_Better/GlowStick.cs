using UnityEngine;
using System.Collections;

public class GlowStick : MonoBehaviour {

	public GameObject greenGlowStick;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			Instantiate(greenGlowStick, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
