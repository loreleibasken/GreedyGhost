using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Pickup : MonoBehaviour {
	public int value = 1;
	public bool shopOnly = false;
	public bool infinite = false;
	public string costType = null;
	public int cost = 0;
	public bool inventoryMode = false;
	public bool hideCrosshairWhileOpen = false;
	public bool dropAtZero = false;
	public bool clickToDrop = false;
	public bool canSpend = false;
	public string displayPrefix = null;
	public AudioClip soundEffect = null;
	//public int reduceValueOnClick = 0;
	private Renderer rend = null;
	private Color defaultColor;
	//private Material[] matDefault,matShop; //failed attempt to use an outline shader instead, unity5 apparently drops materials > 1???
	private AudioSource audioSource;
	public void Start() {
	//public void prepRend() {
		if(!rend) {
			rend = gameObject.GetComponent<Renderer>();
			if(!rend) rend = gameObject.GetComponentInChildren<Renderer>();
			if(rend) defaultColor = rend.material.color;
			else Debug.Log("Error: " + gameObject.name + " shop has no solid material to highlight, add a child cube.");
			//matDefault = rend.materials;
			//matShop = new Material[matDefault.Length + 1];
			//matShop[0] = Inventory.I.shopHover;
			//for(int i = 0;i<matDefault.Length;++i) matShop[i+1] = matDefault[i];
		}
		if(soundEffect) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(player) {
				audioSource = player.GetComponent<AudioSource>();
				if(!audioSource) Debug.Log(gameObject.name + " can't play sounds, the Player needs an AudioSource component.");
			} else Debug.Log(gameObject.name + " can't find the player, make sure it is tagged as Player.");
		}
	}
	public void OnTriggerEnter(Collider c) {
		if(!inventoryMode && !shopOnly && c.tag == "Player") AddToInventory();
	}
	public void OnMouseOver() {
		if(shopOnly) {
			//prepRend(); //fixes a lost material Unity bug
			if(costType != null && Inventory.all.ContainsKey(costType)) {
				if(Inventory.all[costType].count >= cost && Inventory.currentItem && Inventory.currentItem.name == costType && Inventory.currentItem.value >= cost) {
					if(rend) rend.material.color = (Input.GetMouseButton(0) ? Inventory.I.shopClick : Inventory.I.shopHighlight);
					//if(Inventory.I.shopHover) rend.materials = matShop; //rend.materials. = Inventory.I.shopHover;
					Inventory.hoveredStore = this;
				} else {
					if(rend) rend.material.color = defaultColor;
					//rend.materials = matDefault;
				}
			} else Debug.Log("Invalid shop on "+gameObject.name+": costType "+costType+" is not in the Inventory.");
		}
	}
	public void OnMouseExit() {
		if(shopOnly) {
			if(rend) rend.material.color = defaultColor;
			//prepRend(); //fixes a lost material Unity bug
			//rend.materials = matDefault;
			if(Inventory.hoveredStore == this) Inventory.hoveredStore = null;
		}
	}
	public void AddToInventory() {
		gameObject.SetActive(infinite);
		Match m = Regex.Match(name,@"(.+?)(?:\s\(\d+\))?$");
		if(m.Success) {
			string itemname = m.Groups[1].Value;
			int index; Inventory.StoredItem item;
			if(Inventory.all.ContainsKey(itemname)) {
				item = Inventory.all[itemname];
				index = Inventory.list.IndexOf(item);
				if(index < 0) { //add to the end of the list if not there already
					index = Inventory.list.Count; Inventory.list.Add(item);
					if(Inventory.I.autoSwitchOnNewPickup) Inventory.index = index; //auto-switch to item if switch mode is enabled
				}
				item.script.value = (item.count += value);
				if(soundEffect && audioSource) audioSource.PlayOneShot(soundEffect);
			} else Debug.Log("Unable to find "+gameObject.name+" in Inventory");
		} else Debug.Log("GameObject name match failed!?");
	}
	public void ChangeValueBy(int i) {
		Inventory.list[Inventory.index].count = value = Mathf.Max(0,value + i);
	}
}
