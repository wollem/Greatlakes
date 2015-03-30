#define DIRECTION_BASED
//#define COLLIDER_BASED

using UnityEngine;
using System.Collections;

public class LookToMove : MonoBehaviour {
	bool moving = false;
	
	public float moveTime = 2f;
	public float maxTurnDist = 20f;
	public float speed = 3f;
	float timer = 0f;

	public Transform cam;

	Vector3 lookDir;

	CharacterController controller;

#if DIRECTION_BASED


	void Start () {
		controller = GetComponent<CharacterController> ();
		moving = false;
//		cam = GetComponentInChildren<Camera> ();
		lookDir = cam.transform.eulerAngles;
	}
	
	void Update () {

		if(Vector3.Distance(lookDir, cam.transform.eulerAngles) > maxTurnDist) {
			lookDir = cam.transform.eulerAngles;
			timer = 0f;
			moving = false;
		}

		if(!moving) {
			timer += Time.deltaTime;
			if(timer > moveTime) {
				moving = true;
			}
		}

		if(moving) {
			controller.SimpleMove(cam.transform.forward * speed);
		}
	}
#endif

#if COLLIDER_BASED

	Collider target;

	Vector3 startPos, endPos;

	void Start () {
		cam = GetComponentInChildren<Camera> ();
	}
	
	void Update () {
		if (!moving) {
			RaycastHit hit;
			if (Physics.Raycast (cam.ScreenPointToRay (new Vector3 (Screen.width / 2f, Screen.height / 2f)), out hit)) {
//				if (target == hit.collider && hit.transform.tag == "MoveToObject") {
				if (target == hit.collider) {
					timer += Time.deltaTime;
					if (timer > moveTime) {
						MoveTo (target);
					}
				} else {
					target = hit.collider;
					timer = 0f;
				}
			}
		} else {
			transform.position = Vector3.Lerp(startPos, endPos, timer);
			timer += Time.deltaTime;
			if(timer > 1f) {
				timer = 1f;
				transform.position = Vector3.Lerp(startPos, endPos, timer);
				moving = false;
			}
		}
	}

	void MoveTo(Collider target) {
		moving = true;
		startPos = transform.position;
		endPos = target.transform.position - (transform.position - target.transform.position).normalized; //MIGHT BE BACKWARDS.
//		endPos = target.transform.position - (target.transform.position - transform.position).normalized; //IF SO, TRY ME INSTEAD
	}
#endif
}
