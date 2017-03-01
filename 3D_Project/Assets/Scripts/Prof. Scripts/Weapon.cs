using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public float projectileTimeout = 0;
	public float damage = 10;
	public List<string> requiredTags = new List<string>();
	public List<string> ignoredTags = new List<string>();
	public float headshotFactor = 1;
	public bool hitOnImpactEnter = false;
	public bool hitOnTriggerEnter = false;
	public float hitCooldown = 0.5f;
	public bool hitDestroys = true;
	public GameObject hitSpawnObject = null;
	public Vector3 spawnOffset = Vector3.zero;
	public Vector3 spawnVelocity = Vector3.zero;
	public Vector3 spawnRotateScale = Vector3.zero;
	[HideInInspector] public float t = 0;
	public AudioClip soundEffect = null;
	private AudioSource audioSource;
	private float tlasthit = 0;
	public void Start() {
		if(soundEffect) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player) {
				audioSource = player.GetComponent<AudioSource>();
				if(!audioSource) Debug.Log(gameObject.name + " can't play sounds, the Player needs an AudioSource component.");
			} else Debug.Log(gameObject.name + " can't find the player, make sure it is tagged as Player.");
		}
	}
	void Update() {
		if(projectileTimeout > 0) {
			t += Time.deltaTime;
			if(t >= projectileTimeout) Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider c) {
		if(hitOnTriggerEnter) Hit(c,false);
	}
	void OnCollisionEnter(Collision c) {
		if(hitOnImpactEnter) Hit(c.collider,c.collider.tag != "Player");
	}
	void Hit(Collider c, bool destroy) {
		if(hitCooldown > 0 && tlasthit + hitCooldown > Time.unscaledTime) return;
		tlasthit = Time.unscaledTime; //stop repeated hits on same enemy
		if(TagHit(c.tag)) {
			destroy = true;
			Health hscr = c.GetComponent<Health>(); AdvancedCharacter acscr = GetComponent<AdvancedCharacter>();
			if(hscr) hscr.Damage(damage * ((acscr && acscr.headshotCollider && c == acscr.headshotCollider) ? headshotFactor : 1));
			if(acscr) acscr.Hit(transform.position);
			if(hitSpawnObject) { //for effects at collision point
				Camera cam = Camera.main;
				Vector3 cx = cam.transform.right, cy = cam.transform.up, cz = cam.transform.forward;
				GameObject spobj = Instantiate(hitSpawnObject); //instantiated without parent, so need to offset from full position
				spobj.transform.localPosition = transform.position + spawnOffset.x*cx + spawnOffset.y*cy + spawnOffset.z*cz;
				if(spawnRotateScale.sqrMagnitude > 0) spobj.transform.eulerAngles = Vector3.Scale(spawnRotateScale,transform.eulerAngles);
				Rigidbody rb = spobj.GetComponent<Rigidbody>();
				if(rb) rb.velocity = spawnVelocity.x*cx + spawnVelocity.y*cy + spawnVelocity.z*cz; //for things like sparks
			}
			if(soundEffect && audioSource) audioSource.PlayOneShot(soundEffect);
		}
		if(hitDestroys && destroy) Destroy(gameObject);
	}
	bool TagHit(string s) {
		if(requiredTags != null && requiredTags.Count > 0) {
			foreach(string rt in requiredTags) {
				if(s == rt) return true;
			}
			return false;
		}
		if(ignoredTags != null && ignoredTags.Count > 0) {
			foreach(string it in ignoredTags) {
				if(s == it) return false;
			}
		}
		return true;
	}
}
