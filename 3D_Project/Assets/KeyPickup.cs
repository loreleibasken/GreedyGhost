using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour {

    public bool haskey;
    public GameObject basementkey;
    public GameObject lockeddoor;
    public GameObject basementkeycanvas;
    public GameObject Basementdoorlocked;
    public GameObject Basementdooropen;


    IEnumerator KeyCanvas()
    {
        basementkeycanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        basementkeycanvas.SetActive(false);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BasementKey")
        {
            StartCoroutine(KeyCanvas());
            Basementdoorlocked.SetActive(false);
            Basementdooropen.SetActive(true);
            basementkey.SetActive(false);
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
