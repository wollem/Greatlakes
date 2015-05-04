using UnityEngine;
using System.Collections;

public class ActivatePushZone : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player") {
			GetComponentInChildren<PushBox>().active = true;
			GetComponent<Animation>().Play("BoatFalling");
		}
	}
}
