using UnityEngine;
using System.Collections;

public class PushBox : MonoBehaviour {

	public Vector3 pushDirection;
	public float strength;

	void OnTriggerStay(Collider other) {
		CharacterController cc = other.GetComponent<CharacterController>();
		if(null != cc) {
			float moddedStrength = strength / Vector3.Distance(other.transform.position, transform.position - pushDirection);
			cc.Move(pushDirection*moddedStrength);
			print (moddedStrength);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		DrawArrow(transform.position, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position + Vector3.Cross(pushDirection, transform.up) * 2.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position + Vector3.Cross(pushDirection, transform.up) * 5.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position + Vector3.Cross(pushDirection, transform.up) * 7.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position - Vector3.Cross(pushDirection, transform.up) * 2.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position - Vector3.Cross(pushDirection, transform.up) * 5.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
		DrawArrow(transform.position - Vector3.Cross(pushDirection, transform.up) * 7.5f, pushDirection, Vector3.Cross(pushDirection, transform.up), 4f);
	}

	public static void DrawArrow(Vector3 pos, Vector3 forward, Vector3 right, float length) {
		Gizmos.DrawRay(pos, forward * length);
		Gizmos.DrawRay(pos + forward * length, (right - forward) * length/4f);
		Gizmos.DrawRay(pos + forward * length, (-right - forward) * length/4f);
	}
}
