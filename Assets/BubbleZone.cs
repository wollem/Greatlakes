using UnityEngine;
using System.Collections;

public class BubbleZone : MonoBehaviour {

	public GameObject bubbleParticles;
	public float bubbleDestroyTime = 2f;

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player") {
			Transform player = col.transform;
			GameObject obj = (GameObject)Instantiate(bubbleParticles, player.position, player.rotation);
			obj.transform.parent = player;
//			Destroy(obj, bubbleDestroyTime);
		}
	}
}
