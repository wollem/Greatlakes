using UnityEngine;
using System.Collections;

public class SeaPerch : MonoBehaviour {

	public Transform confinedArea;

	enum State { Idle, Moving };

	State state = State.Idle;

	Vector3 target;
	Vector3 prevPosition;

	public float minDistance = 0.4f;
	public float rotateSpeed = 1.0f;
	public float moveSpeed = 2.0f;
	public float minIdleTime = 0.7f;
	public float maxIdleTime = 1.5f;

	float curIdleTime;
	float timer = 0f;

	void Start () {
		state = State.Moving;
		prevPosition = transform.position;
		target = RandomPosition();
	}
	
	void Update () {
		if(state == State.Moving) {
			Vector3 dir = (target - prevPosition).normalized;
			transform.position += dir * moveSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
			if(Vector3.Distance(transform.position, target) < minDistance) {
				state = State.Idle;
				curIdleTime = Random.Range(minIdleTime, maxIdleTime);
			}
		} else {
			timer += Time.deltaTime;
			if(timer > curIdleTime) {
				timer = 0f;
				state = State.Moving;
				prevPosition = transform.position;
				target = RandomPosition();
			}
		}
	}

	Vector3 RandomPosition() {
		Vector3 min = confinedArea.GetComponent<Collider>().bounds.min;
		Vector3 max = confinedArea.GetComponent<Collider>().bounds.max;
		return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
	}
}
