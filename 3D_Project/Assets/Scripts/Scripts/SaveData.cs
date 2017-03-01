using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour {
	static public Dictionary<string,object> data = new Dictionary<string, object>();
	[Serializable]
	public class SaveObject {
		public bool isParentOfGroupInstead = false;
		public GameObject obj;
		public bool saveTransform = true;
		public bool saveHealthScript = false;
		public bool saveAdvancedCharacterState = false;
	}
	public bool autoSaveIfEmpty = true;
	public DialogTree savedTree = null;
	public Inventory savedInventory = null;
	public List<SaveObject> objects = new List<SaveObject>();
	public void Awake() { Load(); }
	public void Update() {
		if(autoSaveIfEmpty && data.Count <= 0) { Save(); autoSaveIfEmpty = false; } //in update so everything is initialized already
	}
	public class SavedDT { //dialog tree state
		public bool redirect; public int restate;
		public SavedDT(DialogTree.TextState dt) { redirect = dt.redirect; restate = dt.redirectState; }
		public void Restore(DialogTree.TextState dt) { dt.redirect = redirect; dt.redirectState = restate; }
	}
	public class SavedSD { //scene data
		public List<SavedDT> dialog;
		public List<SavedOb> objects;
		public SavedSD(List<SavedDT> dlist,List<SavedOb> olist) { dialog = dlist; objects = olist; }
		//public void Restore(out List<SavedDT> dlist,List<SavedOb> olist) { dlist = dialog; }
	}
	public class SavedIP { //inventory pickup
		public string name;
		public int count;
		public SavedIP(string n, int c) { name = n; count = c; }
		//public void Restore(Inventory.StoredItem item) {  }
	}
	public class SavedOb { //object data
		public Vector3 lpos,lrot;
		public float health;
		public int charState;
		public SavedOb(GameObject go,SaveObject so) {
			if(so.saveTransform) { lpos = go.transform.localPosition; lrot = go.transform.localEulerAngles; }
			Health h; if(so.saveHealthScript && (h = go.GetComponent<Health>())) health = h.health;
			AdvancedCharacter a; if(so.saveAdvancedCharacterState && (a = go.GetComponent<AdvancedCharacter>())) charState = a.stateIndex;
		}
		public void Restore(GameObject go,SaveObject so) {
			if(so.saveTransform) { go.transform.localPosition = lpos; go.transform.localEulerAngles = lrot; }
			Health h; if(so.saveHealthScript && (h = go.GetComponent<Health>())) h.health = health;
			AdvancedCharacter a; if(so.saveAdvancedCharacterState && (a = go.GetComponent<AdvancedCharacter>())) a.stateIndex = charState;
		}
	}
	public void Save() {
		string scname = "scene_" + SceneManager.GetActiveScene().name;
		List<SavedDT> dlist = null;
		if(savedTree) {
			dlist = new List<SavedDT>();
			foreach(DialogTree.TextState d in savedTree.states) dlist.Add(new SavedDT(d));
		}
		List<SavedOb> olist = new List<SavedOb>();
		foreach(SaveObject so in objects) {
			if(so.isParentOfGroupInstead) {
				for(int i = 0; i < so.obj.transform.childCount; ++i) {
					olist.Add(new SavedOb(so.obj.transform.GetChild(i).gameObject,so));
				}
			} else olist.Add(new SavedOb(so.obj,so));
		}
		SavedSD scenedata = new SavedSD(dlist,olist);
		data[scname] = scenedata; //save scene data at scene index
		if(savedInventory) {
			List<SavedIP> items = new List<SavedIP>();
			foreach(KeyValuePair<string,Inventory.StoredItem> item in Inventory.all) items.Add(new SavedIP(item.Key,item.Value.count));
			data["inventory"] = items;
		}
	}
	public void Load() {
		string scname = "scene_" + SceneManager.GetActiveScene().name;
		SavedSD scenedata = data.ContainsKey(scname) ? (SavedSD)data[scname] : null;
		if(scenedata != null) {
			if(savedTree && scenedata.dialog != null) { //dialog exists and save data exists for it
				for(int i = 0;i < scenedata.dialog.Count;++i) scenedata.dialog[i].Restore(savedTree.states[i]);
			}
			int curobj = 0; //restore saved objects
			for(int i = 0;i < objects.Count;++i) {
				if(objects[i].isParentOfGroupInstead) {
					for(int j = 0;j < objects[i].obj.transform.childCount;++j) {
						scenedata.objects[curobj++].Restore(objects[i].obj.transform.GetChild(j).gameObject,objects[i]);
					}
				} else scenedata.objects[curobj++].Restore(objects[i].obj,objects[i]);
			}
		}
		if(savedInventory) {
			List<SavedIP> items = data.ContainsKey("inventory") ? (List<SavedIP>)data["inventory"] : null;
			if(items != null) { //inventory was saved
				foreach(SavedIP i in items) { //foreach saved item
					if(Inventory.all.ContainsKey(i.name)) Inventory.all[i.name].count = i.count; //if all has item, update count
				}
			}
		}
	}
}
