using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public class StoredItem {
		public Pickup script;
		public int count;
		public float y, t;
		public StoredItem(GameObject go) {
			script = go.GetComponent<Pickup>();
			if(script) { count = script.value; script.inventoryMode = true; }
			else Debug.Log(go.name+" in the inventory is missing a Pickup script. Add it to avoid errors.");
			t = 0; y = go.transform.localPosition.y;
		}
	}
	static public List<StoredItem> list = new List<StoredItem>();
	static public Dictionary<string,StoredItem> all = new Dictionary<string,StoredItem>();
	static public int index = 0;
	static public Pickup hoveredStore, currentItem;
	static public Inventory I;
	public Color shopHighlight = Color.cyan;
	public Color shopClick = new Color(.5f,1,1);
	public Text visualValue = null;
	public Image crosshair = null;
	public bool autoSwitchOnNewPickup = true;
	//public Material shopHover = null;
	void Start() {
		I = this; hoveredStore = null; currentItem = null;
		GameObject go;
		list.Clear(); index = 0;
		foreach(KeyValuePair<string,StoredItem> i in all) { //reset gameobjects on scene changes, but save the count
			all[i.Key].script = null;
			if(all[i.Key].count > 0) list.Add(all[i.Key]);
		}
		for(int i = 0;i<transform.childCount;++i) {
			go = transform.GetChild(i).gameObject;
			if(all.ContainsKey(go.name)) {
				all[go.name].script = go.GetComponent<Pickup>();
				all[go.name].script.value = all[go.name].count;
				all[go.name].script.inventoryMode = true;
			} else {
				all[go.name] = new StoredItem(go);
			}
			go.SetActive(false);
			if(all[go.name].count > 0) {
				all[go.name].script.value = 0; //to fix doubling the default count
				all[go.name].script.AddToInventory();
			}
		}
		if(visualValue) visualValue.text = "";
	}
	void Update() {
		if(visualValue) visualValue.text = "";
		if(crosshair) crosshair.enabled = true;
		if(list.Count > 0) { //you've collected at least one type of item
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			if(scroll != 0) index = (index + 10*list.Count + (scroll == 0 ? 0 : (scroll < 0 ? 1 : -1))) % list.Count; //cycle the list more safely

			bool stayalive; int i = 0; Pickup p, q;
			while(i < list.Count) { //only show the current item selected, but with some fancy motion
				stayalive = (list[i].count > 0 || !list[i].script.dropAtZero);
				if(i == index && stayalive) { //animate t from 0 (hidden) to 1 (visible)
					list[i].t = Mathf.Min(1,list[i].t + 5*Time.deltaTime);
					p = list[i].script;
					currentItem = p; q = hoveredStore;
					//if(p.canSpend && hoveredStore && Input.GetMouseButtonDown(0)) {
					if(p.canSpend && q && q.isActiveAndEnabled && q.costType == p.name && q.cost <= list[i].count && Input.GetMouseButtonDown(0)) {
						if(hoveredStore.value > 0) hoveredStore.AddToInventory();
						else hoveredStore.gameObject.SetActive(hoveredStore.infinite); //for locks that don't have a value
						p.value = (list[i].count -= hoveredStore.cost);
					} else if(p.clickToDrop && Input.GetMouseButtonDown(0)) {
						p.value = list[i].count = 0;
					}
					if(currentItem.displayPrefix != null && currentItem.displayPrefix.Length > 0) {
						if(visualValue) visualValue.text = currentItem.displayPrefix + currentItem.value;
						else Debug.Log("Error: no visual value Text object assigned to inventory script on "+gameObject.name);
					}
					if(crosshair && currentItem.hideCrosshairWhileOpen) crosshair.enabled = false;
				} else {
					if(i == index) currentItem = null;
					list[i].t = Mathf.Max(0,list[i].t - 5*Time.deltaTime);
					stayalive |= list[i].t > 0; //if hidden and removable, do it at loop end
				}
				GameObject go = list[i].script.gameObject;
				if(list[i].t > 0) { //active if animating or selected
					go.SetActive(true);
					Vector3 pos = go.transform.localPosition; pos.y = list[i].y + Mathf.Sin(.5f*Mathf.PI*list[i].t) - 1;
					go.transform.localPosition = pos; //unity must set a new Vector3 to change one value
				} else go.SetActive(false);
				if(stayalive) ++i;
				else { //remove the item from your current inventory (typically when 0 left)
					list.RemoveAt(i);
					if(index >= i) { //proper selected item fallback to slot before, with wraparound
						index--;
						if(index < 0) index = Mathf.Max(0,list.Count-1);
					}
				}
			}
		}
	}
}
