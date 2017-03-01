using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class BasicAction : MonoBehaviour {
	public UnityEvent CallOnStart = null;
	public UnityEvent CallOnUpdate = null;
	public string requiredTag = null;
	public UnityEvent CallOnTriggerEnter = null;
	public UnityEvent CallOnTriggerStay = null;
	public UnityEvent CallOnImpactEnter = null;
	void Start() {
		if(CallOnStart != null) CallOnStart.Invoke();
	}
	void Update() {
		if(CallOnUpdate != null) CallOnUpdate.Invoke();
	}
	void OnTriggerEnter(Collider c) {
		if(CallOnTriggerEnter != null && (requiredTag == null || requiredTag.Length == 0 || c.tag == requiredTag)) CallOnTriggerEnter.Invoke();
	}
	void OnTriggerStay(Collider c) {
		if(CallOnTriggerStay != null && (requiredTag == null || requiredTag.Length == 0 || c.tag == requiredTag)) CallOnTriggerStay.Invoke();
	}
	void OnCollisionEnter(Collision c) {
		if(CallOnImpactEnter != null && (requiredTag == null || requiredTag.Length == 0 || c.gameObject.tag == requiredTag)) CallOnImpactEnter.Invoke();
	}
	public void SwitchSceneByName(string s) { SceneManager.LoadScene(s); }
	static public bool cursorVisible = false;
	static private bool _cvis = true;
	public void SetMouseCursorVisibility(bool on) {
		if(on != _cvis) {
			_cvis = on;
			if(_cvis) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
			else { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
		}
	}
	public void UpdateCursorVisibility() { //external from FPSController so it can actually be changed for dialog, etc
		if(Input.GetKeyUp(KeyCode.Escape)) cursorVisible = true;
		else if(Input.GetMouseButtonUp(0)) cursorVisible = false;
		SetMouseCursorVisibility(cursorVisible);
	}
	public void DestroyObject(GameObject go) { Destroy(go); }
	public void InstantiateObject(GameObject go,Vector3 pos) { Instantiate(go,pos,Quaternion.identity); }
	public void SetObjectVisibility(GameObject go,bool active) { go.SetActive(active); }
	public void SetObjectActive(GameObject go) { go.SetActive(true); }
	public void SetObjectInactive(GameObject go) { go.SetActive(false); }
}
