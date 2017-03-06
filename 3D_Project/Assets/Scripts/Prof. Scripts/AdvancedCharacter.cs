using UnityEngine;
using System.Collections;
#if UNITY_5_5_OR_NEWER
using UnityEngine.AI;
#endif

public class AdvancedCharacter : MonoBehaviour {
	public enum FacingMode { none, target, forward, lookAround };
	public enum PatrolMode { once, pingPongOnce, loop, pingPongLoop };
	public enum MovingMode { none, forward, patrol, seek };
	public enum StateChangeMode { timer, lineOfSight, seekEnd };
	[System.Serializable]
	public class StateChange {
		public StateChangeMode on = StateChangeMode.timer;
		public int toIndex = 0;
		public float delay = 0;
		public Vector3 eyeOffset = Vector3.zero;
		public float maxRange = 15f;
		public float maxAngle = 45f;
		[HideInInspector] public float t;
		public void Reset() {
			t = delay;
		}
		public void UpdateIndex(AdvancedCharacter asc) {
			if(on == StateChangeMode.timer) {
				t = Mathf.Max(0,t-Time.deltaTime);
				if(t == 0) { Reset(); asc.ChangeState(toIndex); }
			} else if(on == StateChangeMode.lineOfSight) {
				Transform teye = asc.gameObject.transform;
				Vector3 eye = teye.position + teye.right*eyeOffset.x + teye.up*eyeOffset.y + teye.forward*eyeOffset.z;
				Vector3 target = asc.state.target.transform.position + asc.state.lookOffset;
				Vector3 ldir = (target - eye).normalized;
				bool found = false;
				if(Vector3.Angle(ldir,asc.gameObject.transform.forward) <= maxAngle) { //within angle of line of sight
					RaycastHit hit;
					if(Physics.Raycast(new Ray(eye,ldir),out hit,maxRange) && hit.collider.gameObject == asc.state.target) { //unobscured and close enough
						found = true; //found the target, but there may be a delay
						t = Mathf.Max(0,t-Time.deltaTime);
						if(t == 0) {
							asc.seekTarget = target;
							Reset(); asc.ChangeState(toIndex);
						}
					}
				}
				if(!found) Reset(); //reset delay when player hides again
			}
		}
	}
	[System.Serializable]
	public class PatrolState {
		public GameObject[] waypoints;
		public int index = 0;
		public PatrolMode mode = PatrolMode.loop;
		[HideInInspector] public Vector3 target;
	}
	[System.Serializable]
	public class CharacterState {// : UnityEngine.Object {
		public StateChange[] changeState;
		public PatrolState patrol;
		public GameObject target;
		public FacingMode facing = FacingMode.forward;
		public bool lookHorizontally = true;
		public bool lookVertically = false;
		public Vector3 lookOffset = new Vector3(0,0.8f,0);
		public MovingMode moving = MovingMode.none;
		public float movingForwardSpeed = 3;
		public bool flying = false;
		public float navDetectionRange = .5f;
		public float damageOnContact = 0;
		public string onlyDamageTag = "";
		public float playerHitKnockback = 6;
		public float enemyHitKnockback = 3;
		public float damageTimeout = .5f;
		//public CharacterState() { SetInspectorDefaults(); }
		////private bool _setdefaults = false;
		//public void SetInspectorDefaults() {
		//	Debug.Log("set defaults");
		//	//if(_setdefaults) return; _setdefaults = true;
		//	facing = FacingMode.forward; lookHorizontally = true; lookVertically = false;
		//	lookOffset = new Vector3(0,0.8f,0); moving = MovingMode.none; movingForwardSpeed = 3; flying = false;
		//	navDetectionRange = .5f; damageOnContact = 0; onlyDamageTag = "";
		//	playerHitKnockback = 6; enemyHitKnockback = 3; damageTimeout = .5f;
		//}
	}
	public GameObject navigator;
	public Collider headshotCollider;
	public int stateIndex = 0;
	public CharacterState[] stateData;
	private CharacterState state;
	//private GameObject ptarget;
	private NavMeshAgent agent = null;
	private int pdir = 1;
	private float dtout = 0;
	private Rigidbody rb = null;
	private Vector3 seekTarget;
	private void Reset() { //called by editor when component is created or reset
		stateData = new CharacterState[] { new CharacterState() }; //start with one state that has default values
		//if(stateData != null) {
		//	Debug.Log("top level reset running");
		//	//foreach(CharacterState cs in stateData) cs.SetInspectorDefaults();
		//	stateData = new CharacterState[] { new CharacterState() };
		//} else Debug.Log("top level reset failed");
	}
	void Start() {
		rb = GetComponent<Rigidbody>();
		if(navigator) {
			if(transform.parent != navigator.transform) Debug.Log("Error: Navigator "+navigator.name+" should be the parent object of "+name);
			agent = navigator.GetComponent<NavMeshAgent>();
			if(!agent) Debug.Log("Error: Navigator "+navigator.name+" needs a NavMeshAgent component!");
		}
		foreach(CharacterState c in stateData) {
			foreach(StateChange s in c.changeState) s.Reset();
			if(c.moving == MovingMode.patrol && !navigator) Debug.Log("Error: Navigator not set on patrolling AdvancedCharacter script on "+name);
		}
		ChangeState(stateIndex);
	}
	void ChangeState(int num) {
		if(num >= 0 && num < stateData.Length) {
			state = stateData[stateIndex = num];
			if(agent) {
				if(state.moving == MovingMode.patrol) {
					agent.autoBraking = false;
					agent.destination = state.patrol.target = state.patrol.waypoints[state.patrol.index].transform.position;
				} else if(state.moving == MovingMode.seek) {
					agent.autoBraking = true;
					agent.destination = seekTarget;
				}
			}
		} else Debug.Log("Error: "+name+"attempted to change character state to "+num+", which is out of bounds.");
	}
	void FixedUpdate() {
		dtout = Mathf.Max(0,dtout - Time.deltaTime);
		switch(state.moving) {
		case MovingMode.forward:
			Vector3 move = transform.forward;
			//if(!state.flying) move.y = 0;
			move *= state.movingForwardSpeed * Time.deltaTime;
			if(agent) agent.velocity = move;
			else if(rb) {
				//if(!state.flying) Debug.Log(name+": "+(50*move));
				rb.AddForce(50*move); //is this function just completely broken? UNITY!
				//rb.velocity = 50*move; //this works but overrides all forces added. GREAT...
			} else transform.localPosition = transform.localPosition + move;
			break;
		case MovingMode.seek: //state targets a single dynamic position
			if((agent.transform.position - seekTarget).magnitude <= state.navDetectionRange || agent.velocity.magnitude <= 0.001f) {
				agent.autoBraking = true;
				bool stopseek = true;
				foreach(StateChange s in state.changeState) {
					if(s.on == StateChangeMode.seekEnd) {
						stopseek = false;
						s.t = Mathf.Max(0,s.t-Time.deltaTime);
						if(s.t == 0) { s.Reset(); ChangeState(s.toIndex); }
						break;
					}
				}
				if(stopseek) state.moving = MovingMode.none;
			}
			break;
		case MovingMode.patrol: //state has a patrol route
			if((agent.transform.position - state.patrol.target).magnitude <= state.navDetectionRange) {
				state.patrol.target = state.patrol.waypoints[state.patrol.index].transform.position; //to return to a patrol after a trigger, check again with the patrol position
				if((agent.transform.position - state.patrol.target).magnitude <= state.navDetectionRange) {
					state.patrol.index += pdir;
					if(state.patrol.index >= state.patrol.waypoints.Length) { //passed the end of the list
						if((state.patrol.mode & PatrolMode.pingPongOnce) > 0) { //ping pongs bounce back from the end of the list
							state.patrol.index = Mathf.Max(0,state.patrol.waypoints.Length-2); pdir = -1;
						} else {
							state.patrol.index = 0; //regular loops go to the start next
							if(state.patrol.mode == PatrolMode.once) { //and once stops the retargetting
								state.moving = MovingMode.none;
								agent.autoBraking = true;
							}
						}
					} else if(state.patrol.index < 0) { //passed the beginning of the list
						state.patrol.index = 0; pdir = 1; //only ping pongs get here, so just bounce back
						if(state.patrol.mode == PatrolMode.pingPongOnce) { //and stop on single loops
							state.moving = MovingMode.none;
							agent.autoBraking = true;
						}
					}
					if(state.moving == MovingMode.patrol) agent.destination = state.patrol.waypoints[state.patrol.index].transform.position;
				}
			}
			break;
		}
		Vector3 rot = Vector3.zero;
		if(state.facing == FacingMode.target) {
			if(state.target) rot = state.target.transform.position + state.lookOffset - transform.position;
			else Debug.Log("Error: "+name+" can't face target because target is undefined in state "+stateIndex+".");
		} else if(state.facing == FacingMode.forward) {
			if(agent) rot = agent.velocity;
			else if(rb) rot = rb.velocity;
		}
		if(rot != Vector3.zero) rot = Quaternion.LookRotation(rot).eulerAngles;
		if(!state.lookVertically) rot.x = 0;
		if(!state.lookHorizontally) rot.y = 0;
		if(state.facing != FacingMode.none) transform.localEulerAngles = rot;
		foreach(CharacterState c in stateData) {
			foreach(StateChange s in c.changeState) s.UpdateIndex(this);
		}
	}
	public void Hit(Vector3 pos) {
		Vector3 knockback = transform.position - pos;
		knockback.Normalize();
		if(!state.flying) knockback.y = .5f;
		if(agent) agent.velocity = state.enemyHitKnockback * knockback;
		if(rb) rb.AddForce((state.enemyHitKnockback / Time.deltaTime) * knockback);
	}
	void OnTriggerStay(Collider c) {
		if(dtout == 0 && state.damageOnContact != 0 && (state.onlyDamageTag.Length == 0 || c.tag == state.onlyDamageTag)) {
			//c.gameObject.SendMessage("Damage",damageOnContact,SendMessageOptions.DontRequireReceiver); //only sends on object with collider
			Health hscript = c.gameObject.GetComponent<Health>();
			if(!hscript) hscript = c.gameObject.GetComponentInChildren<Health>();
			if(hscript) hscript.Damage(state.damageOnContact);
			if(state.playerHitKnockback > 0) {
				//can't knockback player because default FPSController has no physics system support, so knockback enemy
				Vector3 knockback = transform.position - c.transform.position;
				knockback.Normalize();
				if(!state.flying) knockback.y = .5f;
				if(agent) agent.velocity = state.playerHitKnockback * knockback;
				if(rb) rb.AddForce((state.playerHitKnockback / Time.deltaTime) * knockback);
				//Rigidbody rb = GetComponent<Rigidbody>();
				//if(rb) rb.AddForce(playerHitKnockback * knockback);
				//if(playerHitSfx) audioSource.PlayOneShot(playerHitSfx);
			}
			dtout = state.damageTimeout;
		}
	}
}
