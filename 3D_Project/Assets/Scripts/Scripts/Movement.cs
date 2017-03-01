using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
	[Serializable]
	public class CameraSettings {
		public Transform camera = null;
		public float smoothTime = 18f;
		public float topLimit = -90f;
		public float bottomLimit = 90f;
		public float xSpeed = 2f;
		public float ySpeed = 2f;
		public Transform thirdPersonAnchor = null;
		public float rotateToFaceMoveSpeed = 2f;
	}
	[Serializable]
	public class MoveSettings {
		public KeyCode kForward = KeyCode.None;
		public KeyCode kBack = KeyCode.None;
		public KeyCode kLeft = KeyCode.None;
		public KeyCode kRight = KeyCode.None;
		public float groundSpeed = 4f;
		public float maxSlopeAngle = 45f;
		public float airFactor = 1f;
		//public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0,0),new Keyframe(.035f,0),new Keyframe(.173f,.424f),new Keyframe(.25f,1));
		//public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0,0),new Keyframe(.035f,0),new Keyframe(.25f,1,-.77f,.77f)); //doesn't work. UNITY!
		public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0,0),new Keyframe(.035f,0),new Keyframe(.5f,1));
		public float stopSlowFactor = .75f;
		public float stopFastFactor = 1.5f;
		public KeyCode kRun = KeyCode.None;
		public float runFactor = 2f;
		public KeyCode kJump = KeyCode.None;
		public float jumpForce = 50f;
		public Vector3 gravity = new Vector3(0,-1,0);
		public float maxFallSpeed = 15f;
		public float slopeStickFactor = 1f;
	}
	public CameraSettings cameraSettings = new CameraSettings();
	[HideInInspector] public Vector3 rot, camrot, pdif;
	[HideInInspector] public float tac = 0, t = 0;
	public MoveSettings moveSettings = new MoveSettings();
	[HideInInspector] public Rigidbody rb;
	[HideInInspector] public Vector3 mdir, hvel, vvel;
	[HideInInspector] public bool gravity, onground = false;
	[HideInInspector] public Transform initParent;
	void Start() {
		if(cameraSettings.thirdPersonAnchor != null) {
			rot = cameraSettings.thirdPersonAnchor.transform.localEulerAngles;
			if(cameraSettings.thirdPersonAnchor.parent = transform) {
				cameraSettings.thirdPersonAnchor.parent = transform.parent;
				pdif = cameraSettings.thirdPersonAnchor.localPosition - transform.position;
			}
		} else rot = transform.localEulerAngles;
		initParent = transform.parent;
		if(cameraSettings.camera) camrot = cameraSettings.camera.localEulerAngles;
		foreach(Keyframe k in moveSettings.accelerationCurve.keys) tac = Mathf.Max(tac,k.time); //this is the only way to get the max curve time? UNITY!
		rb = GetComponent<Rigidbody>();
		mdir = Vector3.zero; hvel = Vector3.zero; vvel = Vector3.zero;
		gravity = rb.useGravity; rb.useGravity = false; //override this buggy mess with some custom code (Unity default gravity falls through ground...)
	}
	void Update() { //handles input and per-frame (~60 frames per second)
		if(DialogTree.I != null && DialogTree.I.currentState >= 0) { mdir = Vector3.zero; return; }
		if(cameraSettings.camera) {
			float xin = Input.GetAxis("Mouse X") * cameraSettings.xSpeed, yin = Input.GetAxis("Mouse Y") * cameraSettings.ySpeed;
			camrot.x = Mathf.Clamp(camrot.x -= yin,cameraSettings.topLimit,cameraSettings.bottomLimit);
			rot.y = rot.y + xin;
			if(cameraSettings.thirdPersonAnchor != null) cameraSettings.thirdPersonAnchor.localEulerAngles = rot;
			else transform.localEulerAngles = rot;
			cameraSettings.camera.localEulerAngles = camrot;
		}
		MoveSettings ms = moveSettings;
		if(cameraSettings.thirdPersonAnchor != null) {
			mdir = cameraSettings.thirdPersonAnchor.right * (((ms.kLeft != KeyCode.None && Input.GetKey(ms.kLeft)) ? -1 : 0) +
									  ((ms.kRight != KeyCode.None && Input.GetKey(ms.kRight)) ? 1 : 0)) +
				   cameraSettings.thirdPersonAnchor.forward * (((ms.kBack != KeyCode.None && Input.GetKey(ms.kBack)) ? -1 : 0) +
										((ms.kForward != KeyCode.None && Input.GetKey(ms.kForward)) ? 1 : 0));
			if(cameraSettings.rotateToFaceMoveSpeed > 0 && mdir.sqrMagnitude > 0) {
				//transform.forward = mdir;//Vector3.RotateTowards(transform.forward,mdir,(180f/Mathf.PI)*cameraSettings.rotateToFaceMoveSpeed,0);
				transform.forward = Vector3.RotateTowards(transform.forward,mdir.normalized,cameraSettings.rotateToFaceMoveSpeed*(Mathf.PI/180f),0);
				//float y = transform.forward.y; Vector3.
				//if(y != )
				//Debug.Log("Angle dot:" + Vector3.Dot(transform.forward,mdir));
				//float tang = Vector3.Dot(transform.right,mdir), ts = Mathf.Sign(tang); tang = Mathf.Min(Mathf.Abs(tang),cameraSettings.rotateToFaceMoveSpeed);
				//if(tang > 0) {
				//	Vector3 lr = transform.localEulerAngles; lr.y += 180*ts*tang;
				//	transform.localEulerAngles = lr;
				//}
			}
			cameraSettings.thirdPersonAnchor.localPosition = transform.position + pdif;
		} else
			mdir = transform.right * (((ms.kLeft != KeyCode.None && Input.GetKey(ms.kLeft)) ? -1 : 0) +
									  ((ms.kRight != KeyCode.None && Input.GetKey(ms.kRight)) ? 1 : 0)) +
				   transform.forward * (((ms.kBack != KeyCode.None && Input.GetKey(ms.kBack)) ? -1 : 0) +
										((ms.kForward != KeyCode.None && Input.GetKey(ms.kForward)) ? 1 : 0));
	}
	void FixedUpdate() { //handles physics (always 50 times per second)
		MoveSettings ms = moveSettings;
		Vector3 move = Vector3.zero;
		//horizontal movement
		//vel = rb.velocity; vel.y = 0;
		if(0 < mdir.sqrMagnitude) {
			float ang = (hvel == Vector3.zero) ? 0 : Vector3.Angle(hvel,mdir);
			if(ang <= 45) { //same keys pressed, accelerate
				t = Mathf.Min(tac, t + Time.deltaTime * (onground ? 1 : ms.airFactor));
				if(t > 0) move = (ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * mdir.normalized;
				//if(t > 0) rb.AddForce((ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * mdir.normalized);
			} else if(ang >= 135) { //opposite keys pressed, decelerate fast
				t = Mathf.Max(0,t - ms.stopFastFactor * Time.deltaTime * (onground ? 1 : ms.airFactor));
				if(t > 0) move = (ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * hvel.normalized;
				//if(t > 0) rb.AddForce((ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * vel.normalized);
			} else { //middling keys pressed, change inertia at same speed until closer angle
				move = Vector3.Slerp(hvel,mdir,.2f);
				//rb.AddForce((ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * mdir.normalized);
			}
			if(ms.kRun != KeyCode.None && Input.GetKey(ms.kRun)) move *= ms.runFactor;
		} else { //no keys pressed, decelerate slow
			t = Mathf.Max(0,t - ms.stopSlowFactor * Time.deltaTime * (onground ? 1 : ms.airFactor));
			if(t > 0) move = (ms.groundSpeed * ms.accelerationCurve.Evaluate(t)) * hvel.normalized;
			//float len = vel.magnitude;
			//if(t > 0) rb.AddForce((Mathf.Min(ms.groundSpeed * ms.accelerationCurve.Evaluate(t),len)/len) * vel);
		}
		hvel = move; //save all the horizontal velocities for the next cycle
		//vertical movement
		if(onground && ms.kJump != KeyCode.None && Input.GetKey(ms.kJump)) { //jump needs to be here, so use GetKey to avoid sticking input
			vvel = ms.jumpForce * Vector3.up;
			onground = false;
		}
		if(gravity) {
			if(onground) vvel = (ms.slopeStickFactor * Time.deltaTime) * ms.gravity; //ground sticking force, for slopes
			else {
				vvel = vvel + ms.gravity;
				if(vvel.y < 0) vvel = Vector3.ClampMagnitude(vvel,ms.maxFallSpeed * ms.gravity.magnitude);
			}
		}
		move += vvel;
		rb.velocity = move; //set velocity once at the end, with all move forces applied
		onground = false; //to support quarter pipes and variable slope terrain, must reset ground every frame
	}
	void OnCollisionStay(Collision c) {
		if(!onground) {
			float ang;
			foreach(ContactPoint cp in c.contacts) {
				if(moveSettings.maxSlopeAngle >= (ang = Vector3.Angle(cp.normal,Vector3.up))) {
					if(cp.otherCollider.tag == "Platform") {
						transform.parent = cp.otherCollider.transform;
						hvel = Vector3.zero; //stop automatically when on a platform
					}
					onground = true; break;
				} else if(vvel.y > 0 && 135 <= ang) vvel.y = 0; //bounce off ceilings
			}
		}
	}
	void OnCollisionExit(Collision c) {
		//onground = false; //fix for walking off cliffs, recalculate ground whenever you leave a collider (now always set)
		if(transform.parent != initParent) transform.parent = initParent; //for walking or jumping off moving platforms and returning to worldspace
	}
}
