using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DialogTree : MonoBehaviour {
	static public DialogTree I = null;
	public bool manageCursor = true;
	public KeyCode escapeKey = KeyCode.None;
	public Color colorHighlight = Color.cyan;
	public Color colorClick = new Color(.5f,1,1);
	public GameObject dialogGroup = null;
	public Text mainText, textChoice1, textChoice2, textChoice3, textChoice4;
	public Button buttonChoice1, buttonChoice2, buttonChoice3, buttonChoice4;
	[Serializable]
	public class TextState {
		public string text, choice1, choice2, choice3, choice4;
		public bool nextOnClick = true, redirectOnClick = false;
		public bool redirect = false;
		public int redirectState = -1;
		public UnityEvent onClick, onChoice1, onChoice2, onChoice3, onChoice4;
	}
	public int currentState = -1;
	public List<TextState> states;
	[HideInInspector] public TextState state;
	void Start() {
		I = this; Hide();
	}
	public void Hide() {
		if(dialogGroup) dialogGroup.SetActive(false); //should set all sub nodes to be invisible
		state = null;
	}
	public void State(int i) {
		if(i >= states.Count) Debug.Log("Error in DialogTree script on "+gameObject.name+" at state "+currentState+": state "+i+" is out of range.");
		else currentState = i;
	}
	public void RedirectSetCurrent(bool yes) { if(state != null) state.redirect = yes; }
	public void RedirectOnState(int n) {
		if(n < states.Count) states[n].redirect = true;
		else Debug.Log("Error in DialogTree script on "+gameObject.name+" at state "+currentState+": turning off redirect on state "+n+", out of range.");
	}
	public void RedirectOffState(int n) {
		if(n < states.Count) states[n].redirect = false;
		else Debug.Log("Error in DialogTree script on "+gameObject.name+" at state "+currentState+": turning off redirect on state "+n+", out of range.");
	}
	public void Choose(int i) {
		if(i == 1) state.onChoice1.Invoke();
		else if(i == 2) state.onChoice2.Invoke();
		else if(i == 3) state.onChoice3.Invoke();
		else if(i == 4) state.onChoice4.Invoke();
	}
	void Update() {
		if(state != null && Input.GetMouseButtonUp(0)) {
			state.onClick.Invoke();
			if(state.redirectOnClick) {
				state.redirect = true;
				if(!state.nextOnClick) currentState = state.redirectState;
			}
			if(state.nextOnClick) {
				currentState = ((currentState + 1) < states.Count) ? (currentState + 1) : -1; //either next valid state or -1
			}
		}
		if(escapeKey != KeyCode.None && Input.GetKeyDown(escapeKey)) currentState = -1;
		int i;
		for(i=0;i<20 && (currentState >= 0 && states[currentState].redirect); ++i) { //max timeout to avoid loops
			currentState = states[currentState].redirectState;
		}
		if(i >= 20) Debug.Log("Error in DialogTree script on "+gameObject.name+" at state "+currentState+": infinite redirect loop timed out.");
		if(currentState >= 0) {
			if(state != states[currentState]) {
				state = states[currentState];
				if(dialogGroup) dialogGroup.SetActive(true);
				bool active;
				if(active = state.text != null && state.text.Length > 0) { mainText.text = state.text; }
				mainText.gameObject.SetActive(active);
				if(active = (state.choice1 != null && state.choice1.Length > 0)) textChoice1.text = state.choice1;
				buttonChoice1.gameObject.SetActive(active); textChoice1.gameObject.SetActive(active);
				if(active = (state.choice2 != null && state.choice2.Length > 0)) textChoice2.text = state.choice2;
				buttonChoice2.gameObject.SetActive(active); textChoice2.gameObject.SetActive(active);
				if(active = (state.choice3 != null && state.choice3.Length > 0)) textChoice3.text = state.choice3;
				buttonChoice3.gameObject.SetActive(active); textChoice3.gameObject.SetActive(active);
				if(active = (state.choice4 != null && state.choice4.Length > 0)) textChoice4.text = state.choice4;
				buttonChoice4.gameObject.SetActive(active); textChoice4.gameObject.SetActive(active);
				if(manageCursor) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
			}
		} else {
			if(state != null) {
				Hide();
				if(manageCursor) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
			}
		}
	}
}
