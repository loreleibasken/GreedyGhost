using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {
	//public float verticalBounce = 0;
	//public float bounceTime = 1;
	//public float spinPerSecond = 0;
	//private float sy, rfactor, sfactor;
	//private Vector3 srot;
	//void Start() {
	//	rfactor = Random.value * bounceTime; sfactor = 2*Mathf.PI/bounceTime;
	//	sy = transform.localPosition.y;
	//	srot = transform.localRotation.eulerAngles;
	//}
	//void Update() {
	//	if(verticalBounce > 0) { //for bouncing powerups
	//		Vector3 pos = transform.localPosition; pos.y = sy + verticalBounce * Mathf.Sin(sfactor * (rfactor + Time.unscaledTime));
	//		transform.localPosition = pos;
	//	}
	//	if(spinPerSecond != 0) { //for spinning powerups
	//		Quaternion rot = transform.localRotation;
	//		rot.eulerAngles = new Vector3(srot.x,srot.y + spinPerSecond * Time.unscaledTime,srot.z);
	//		transform.localRotation = rot;
	//	}
	//}
	public float verticalBounce = 0;
	public float bounceTime = 1;
	public float spinPerSecond = 0;
	public bool scaleToCamera = false;
	public float camScaledHeight = 10;
	public Transform track = null;
	public Vector3 trackFactor = Vector3.one;
	public Vector3 trackOffset = Vector3.zero;
	public float trackMaxDistance = 500;
	private float rfactor, sfactor;//sy,
	private Vector3 spos, srot;
	void Start() {
		rfactor = Random.value * bounceTime; sfactor = 2*Mathf.PI/bounceTime;
		//sy = transform.localPosition.y;
		spos = transform.localPosition;
		if(track != null) spos -= Vector3.Scale(trackFactor,track.position + trackOffset);
		srot = transform.localRotation.eulerAngles;
	}
	void Update() {
		Vector3 pos = spos, pt;// = transform.localPosition; pos.y = spos.y;
		if(track != null) {
			pos += Vector3.Scale(trackFactor,track.position + trackOffset);
			pt = pos - track.position; //distance from tracked to object
			if(pt.sqrMagnitude > trackMaxDistance*trackMaxDistance) pos = track.position + trackMaxDistance * pt.normalized;
		}
		if(verticalBounce > 0) { //for bouncing powerups
			pos.y += verticalBounce * Mathf.Sin(sfactor * (rfactor + Time.unscaledTime));
			transform.localPosition = pos;
		}
		if(spinPerSecond != 0) { //for spinning powerups
			Quaternion rot = transform.localRotation;
			rot.eulerAngles = new Vector3(srot.x,srot.y + spinPerSecond * Time.unscaledTime,srot.z);
			transform.localRotation = rot;
		}
		if(scaleToCamera) {
			Vector3 bot = Camera.main.WorldToScreenPoint(transform.position), top = bot; top.y += camScaledHeight;
			transform.localScale = Vector3.one * Mathf.Max(1,(Camera.main.ScreenToWorldPoint(top)-Camera.main.ScreenToWorldPoint(bot)).magnitude);
		}
	}
}
