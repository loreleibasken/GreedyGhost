using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour {

    public bool haskey;
    public GameObject basementkey;
    public GameObject basementkeycanvas;
    public GameObject Basementdoorlocked;
    public GameObject Basementdooropen;
    public GameObject storagekey;
    public GameObject storagedoorlocked;
    public GameObject storagedooropen;
    public GameObject storagekeycanvas;


    IEnumerator BasementKeyCanvas()
    {
        basementkeycanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        basementkeycanvas.SetActive(false);
    }

    IEnumerator StorageKeyCanvas()
    {
        storagekeycanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        storagekeycanvas.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BasementKey")
        {
            StartCoroutine(BasementKeyCanvas());
            Basementdoorlocked.SetActive(false);
            Basementdooropen.SetActive(true);
            basementkey.SetActive(false);
            haskey = true;
        }
        if (other.tag == "StorageKey")
        {
            StartCoroutine(StorageKeyCanvas());
            storagedoorlocked.SetActive(false);
            storagedooropen.SetActive(true);
            storagekey.SetActive(false);
            haskey = true;
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
