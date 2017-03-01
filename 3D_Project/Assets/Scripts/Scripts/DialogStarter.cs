using System.Collections.Generic;
using UnityEngine;

public class DialogStarter : MonoBehaviour {
	public enum TriggerType { none, autoOnce, triggerEnter, mouseClick, impactEnter }
	public TriggerType actOn = TriggerType.mouseClick;
	public int dialogState = 0;
	public bool destroyOnDialog = false;
	private Renderer rend = null;
	private Color defaultColor;
	public void Start() {
		if(actOn == TriggerType.mouseClick) {
			rend = gameObject.GetComponent<Renderer>();
			if(!rend) rend = gameObject.GetComponentInChildren<Renderer>();
			if(rend) defaultColor = rend.material.color;
			else Debug.Log("Error: " + gameObject.name + " DialogStarter has no solid material to highlight, add a child cube.");
		}
	}
	public void Update() {
		if(actOn == TriggerType.autoOnce) { StartDialog(); actOn = TriggerType.none; }
	}
	void OnMouseOver() {
		if(rend && actOn == TriggerType.mouseClick && DialogTree.I.currentState < 0) {
			if(Input.GetMouseButtonUp(0)) { StartDialog(); rend.material.color = defaultColor; }
			else rend.material.color = (Input.GetMouseButton(0) ? DialogTree.I.colorClick : DialogTree.I.colorHighlight);
		}
	}
	public void OnMouseExit() {
		if(rend && actOn == TriggerType.mouseClick) rend.material.color = defaultColor;
	}
	public void OnTriggerEnter(Collider c) {
		if(actOn == TriggerType.triggerEnter) StartDialog();
	}
	public void OnCollisionEnter(Collision c) {
		if(actOn == TriggerType.impactEnter) StartDialog();
	}
	void StartDialog() {
		if(DialogTree.I.currentState < 0) {
			DialogTree.I.currentState = dialogState;
			if(destroyOnDialog) Destroy(gameObject);
		}
	}
}
