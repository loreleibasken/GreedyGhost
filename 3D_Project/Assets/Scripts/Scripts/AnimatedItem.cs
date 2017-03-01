using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AnimatedItem : MonoBehaviour {
	public enum ToggleMode { none, min, max, animStart, animEnd }
	public enum AnimatedItemTrigger { Automatic, Click, Trigger, Impact, NoLocks, MouseToggle }
	public AnimatedItemTrigger actOn = AnimatedItemTrigger.Click;
	[HideInInspector] public float tact = 0;
	public bool permanent = false;
	public GameObject activatedObject = null;
	public float objectTime = 0;
	public ToggleMode toggleActivatedAt = ToggleMode.none;
	public GameObject spawnObject = null;
	public float spawnDelay = 0;
	public Vector3 spawnOffset = Vector3.zero;
	public Vector3 spawnVelocity = Vector3.zero;
	public Vector3 spawnRotateScale = Vector3.zero;
	//public bool spawnRotated = false;
	public AnimationCurve animPositionX = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animPositionY = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animPositionZ = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animRotationX = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animRotationY = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animRotationZ = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animScaleAll = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animScaleX = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animScaleY = new AnimationCurve(new Keyframe(0,0));
	public AnimationCurve animScaleZ = new AnimationCurve(new Keyframe(0,0));
	public float loopOffset = 0;
	public bool loopReverse = false;
	public UnityEvent onActionCall = null;
	public UnityEvent onFinishCall = null;
	public AudioClip actionStartSfx = null;
	private Vector3 spos, srot, ssc;
	private bool animating = false, spawned, objon;
	private float t;
	private SphereCollider overlapRange;
	private AudioSource audioSource;
	private Renderer rend = null;
	private Color defaultColor;
	void Start() {
		spos = transform.localPosition;
		srot = transform.localEulerAngles;
		ssc = transform.localScale;
		//UNITY is so good, this is the only way to calculate the max animation time automatically...
		foreach(Keyframe k in animPositionX.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animPositionY.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animPositionZ.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animRotationX.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animRotationY.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animRotationZ.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animScaleAll.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animScaleX.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animScaleY.keys) tact = Mathf.Max(tact,k.time);
		foreach(Keyframe k in animScaleZ.keys) tact = Mathf.Max(tact,k.time);
		overlapRange = GetComponent<SphereCollider>();
		if(activatedObject) activatedObject.SetActive(false); //disable the activated object on start, so it can be activated later
		if(actionStartSfx) {
			audioSource = gameObject.GetComponent<AudioSource>();
			if(!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
			if(audioSource) audioSource.playOnAwake = false;
		}
		if(actOn == AnimatedItemTrigger.MouseToggle) {
			rend = gameObject.GetComponent<Renderer>();
			if(!rend) rend = gameObject.GetComponentInChildren<Renderer>();
			if(rend) defaultColor = rend.material.color;
			else Debug.Log("Error: " + gameObject.name + " AnimatedItem has no solid material to highlight, add a child cube.");
		}
	}
	public void OnMouseOver() {
		if(rend && actOn == AnimatedItemTrigger.MouseToggle && DialogTree.I.currentState < 0) {
			if(Input.GetMouseButtonUp(0)) { Toggle(); rend.material.color = defaultColor; } else rend.material.color = (Input.GetMouseButton(0) ? DialogTree.I.colorClick : DialogTree.I.colorHighlight);
		}
	}
	public void OnMouseExit() {
		if(rend && actOn == AnimatedItemTrigger.MouseToggle) rend.material.color = defaultColor;
	}
	float toggleDir = 1;
	void Toggle() { toggleDir *= -1; }
	void Update() {
		if(actOn == AnimatedItemTrigger.MouseToggle) { //override for mouse toggle mode
			t = Mathf.Clamp(t + Time.deltaTime,0,tact);
			if(activatedObject && toggleActivatedAt != ToggleMode.none) {
				if(toggleActivatedAt == ToggleMode.min) activatedObject.SetActive(t <= 0);
				else if(toggleActivatedAt == ToggleMode.max) activatedObject.SetActive(t >= tact);
				else if(toggleActivatedAt == ToggleMode.animStart) activatedObject.SetActive(toggleDir > 0);
				else if(toggleActivatedAt == ToggleMode.animEnd) activatedObject.SetActive(t == 0 || t == tact);
			}
			return;
		}
		if(actOn == AnimatedItemTrigger.NoLocks && !animating && overlapRange) {
			Collider[] hits = Physics.OverlapSphere(overlapRange.transform.position + overlapRange.center,overlapRange.radius);
			int i = 0;
			for(; i < hits.Length; ++i) {
				if(hits[i].tag == "Lock") break;
			}
			if(i >= hits.Length) BeginAction(); //no overlapping locks
		}
		if(permanent && t >= tact) { //stop animating and lockout if permanent

		} else if(animating) {
			t = Mathf.Min(t + Time.deltaTime, tact);
			if(activatedObject && objon && t >= objectTime) { //activated object has a minimum 1 frame delay (good for muzzle flash)
				activatedObject.SetActive(false); //activating ends at the objectTime (it is a total time length)
				objon = false;
			}
			if(spawnObject && !spawned && t >= spawnDelay) NewSpawnObject();
			if(!permanent && t >= tact && actOn != AnimatedItemTrigger.Automatic) { //the action is done, return to normal detection and pre-animation transform
				transform.localPosition = spos;
				transform.localEulerAngles = srot;
				transform.localScale = ssc;
				t = 0; animating = false;
				if(onFinishCall != null) onFinishCall.Invoke();
			} else { //handle animation transformations on gameobject's transform over time
				bool updated = false;
				Vector3 pos = spos;
				float teval = loopOffset + (loopReverse ? -t : t); //for easier looping animation reuse
				if(animPositionX != null && animPositionX.length > 1) { pos.x += animPositionX.Evaluate(teval); updated = true; }
				if(animPositionY != null && animPositionY.length > 1) { pos.y += animPositionY.Evaluate(teval); updated = true; }
				if(animPositionZ != null && animPositionZ.length > 1) { pos.z += animPositionZ.Evaluate(teval); updated = true; }
				if(updated) { transform.localPosition = pos; updated = false; }
				pos = srot;
				if(animRotationX != null && animRotationX.length > 1) { pos.x += animRotationX.Evaluate(teval); updated = true; }
				if(animRotationY != null && animRotationY.length > 1) { pos.y += animRotationY.Evaluate(teval); updated = true; }
				if(animRotationZ != null && animRotationZ.length > 1) { pos.z += animRotationZ.Evaluate(teval); updated = true; }
				if(updated) { transform.localEulerAngles = pos; updated = false; }
				pos = ssc;
				float scall = 0;
				if(animScaleAll != null && animScaleAll.length > 1) {
					scall = animScaleAll.Evaluate(teval); pos.x += scall; pos.y += scall; pos.z += scall; updated = true; }
				if(animScaleX != null && animScaleX.length > 1) { pos.x += animScaleX.Evaluate(teval); updated = true; }
				if(animScaleY != null && animScaleY.length > 1) { pos.y += animScaleY.Evaluate(teval); updated = true; }
				if(animScaleZ != null && animScaleZ.length > 1) { pos.z += animScaleZ.Evaluate(teval); updated = true; }
				if(updated) { transform.localScale = pos; updated = false; }
				if(t >= tact && (permanent || actOn == AnimatedItemTrigger.Automatic) && onFinishCall != null) onFinishCall.Invoke();
				if(!permanent && actOn == AnimatedItemTrigger.Automatic && t >= tact) t = 0; //avoid popping frame on continuous automatic
			}
		} else if(actOn == AnimatedItemTrigger.Automatic || (actOn == AnimatedItemTrigger.Click && Input.GetMouseButtonDown(0))) {
			BeginAction();
		}
	}
	void OnTriggerEnter(Collider c) { if(!animating && actOn == AnimatedItemTrigger.Trigger) BeginAction(); }
	void OnCollisionEnter(Collision c) { if(!animating && actOn == AnimatedItemTrigger.Impact) BeginAction(); }
	public void BeginAction() {
		t = 0; animating = true;
		if(spawnObject && spawnDelay <= 0) { //spawn immediately if necessary
			NewSpawnObject();
		} else spawned = false;
		if(activatedObject) {
			activatedObject.SetActive(true);
		}
		if(onActionCall != null) onActionCall.Invoke();
		if(actionStartSfx && audioSource) audioSource.PlayOneShot(actionStartSfx);
	}
	public void NewSpawnObject() {
		Camera cam = Camera.main;
		Vector3 cx = cam.transform.right, cy = cam.transform.up, cz = cam.transform.forward;
		GameObject spobj = Instantiate(spawnObject); //instantiated without parent, so need to offset from full position
		spobj.transform.localPosition = transform.position + spawnOffset.x*cx + spawnOffset.y*cy + spawnOffset.z*cz;
		if(spawnRotateScale.sqrMagnitude > 0) spobj.transform.eulerAngles = Vector3.Scale(spawnRotateScale,transform.eulerAngles);
		//if(spawnRotated) spobj.transform.rotation = transform.rotation;
		Rigidbody rb = spobj.GetComponent<Rigidbody>();
		if(rb) rb.velocity = spawnVelocity.x*cx + spawnVelocity.y*cy + spawnVelocity.z*cz; //for things like grenades
		spawned = true;
	}
}
