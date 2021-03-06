﻿#define DIRECTION_BASED
//#define COLLIDER_BASED

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LookToMove : MonoBehaviour {
	bool moving = false;
	
	public float moveTime = 2f;
	public float maxTurnDist = 20f;
	public float speed = 3f;
	public float turnSpeed = 1f;
	float timer = 0f;

	public Transform cam;

	Vector3 lookDir;

	CharacterController controller;

	UnityStandardAssets.ImageEffects.BlurOptimized[] blurs;

	public Vector3 mapCenter = Vector3.zero;
	public float radius = 1000f;
	public float blurIncrementAmount = 3f;
	public float darkIncrementAmount = 5f;

	public Renderer blackRenderer;

	public Transform forwardDirection, leftEyeAnchor, rightEyeAnchor;

	public float turnBuffer = 5.0f;

	public GameObject spriteStand, spriteWalk;
	public RectTransform bar;

#if DIRECTION_BASED

	void Start () {
		controller = GetComponent<CharacterController>();
		moving = false;
		lookDir = cam.transform.eulerAngles;
		blurs = GetComponentsInChildren<UnityStandardAssets.ImageEffects.BlurOptimized>();

//		forwardDirection = transform.FindChild("ForwardDirection");
//		leftEyeAnchor = transform.FindChild("LeftEyeAnchor");
//		rightEyeAnchor = transform.FindChild("RightEyeAnchor");
	}
	
	void Update () {
//		if(Mathf.Abs(forwardDirection.eulerAngles.y%360 - leftEyeAnchor.eulerAngles.y%360) > turnBuffer) {
			float rotateAmount = (forwardDirection.eulerAngles.y) - (leftEyeAnchor.eulerAngles.y);
//			print(rotateAmount);
		float diff = Vector2.Angle (new Vector2 (forwardDirection.forward.x, forwardDirection.forward.z), new Vector2 (leftEyeAnchor.forward.x, leftEyeAnchor.forward.z));
		if(diff>turnBuffer){
			transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(leftEyeAnchor.forward),Time.deltaTime*turnSpeed);
//			if(Mathf.Sign(rotateAmount) > 0)
//				rotateAmount -= turnBuffer;
//			else
//				rotateAmount += turnBuffer;
//			transform.Rotate(0f, -rotateAmount, 0f);
		}

		if(Vector3.Distance(transform.position, mapCenter) > radius) {
			float blurAmount = ((Vector3.Distance(transform.position, mapCenter) - radius)/blurIncrementAmount);
			for(int i = 0; i < blurs.Length; i++) {
				blurs[i].blurIterations = (int)blurAmount;
			}
			Color blackColor =	blackRenderer.material.color;
			blackColor.a = blurAmount / darkIncrementAmount;
			blackRenderer.material.color = blackColor;
		}

		if(Vector3.Distance(lookDir, cam.transform.eulerAngles) > maxTurnDist) {
			lookDir = cam.transform.eulerAngles;
			timer = 0f;
			moving = false;
			bar.gameObject.SetActive(true);
			spriteStand.SetActive(true);
			spriteWalk.SetActive(false);
		}

		if(!moving) {
			bar.sizeDelta = new Vector2((timer/moveTime)*10f, bar.rect.height);
			timer += Time.deltaTime;
			if(timer > moveTime) {
				moving = true;
				bar.gameObject.SetActive(false);
				spriteStand.SetActive(false);
				spriteWalk.SetActive(true);
			}
		}

		if(moving) {
			controller.SimpleMove(cam.transform.forward * speed);
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(mapCenter, Vector3.one * 10f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(mapCenter, radius);
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
