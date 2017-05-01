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
    public GameObject secretroomkey;
    public GameObject secretroomdooropen;
    public GameObject secretroomdoorlocked;
    public GameObject secretroomcanvas;
    public GameObject studykey;
    public GameObject studydoorlocked1;
    public GameObject studydoorlocked2;
    public GameObject studydooropen1;
    public GameObject studydooropen2;
    public GameObject studycanvas;
    public GameObject denkey;
    public GameObject dendoorclosed;
    public GameObject dendooropen;
    public GameObject dendoorcanvas;


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

    IEnumerator SecretRoomKeyCanvas()
    {
        secretroomcanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        secretroomcanvas.SetActive(false);
    }

    IEnumerator StudyKeyCanvas()
    {
        studycanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        studycanvas.SetActive(false);
    }

    IEnumerator DenKeyCanvas()
    {
        dendoorcanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        dendoorcanvas.SetActive(false);
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
        if (other.tag == "StudyKey")
        {
            StartCoroutine(StudyKeyCanvas());
            studydoorlocked1.SetActive(false);
            studydoorlocked2.SetActive(false);
            studydooropen1.SetActive(true);
            studydooropen2.SetActive(true);
            studykey.SetActive(false);
            haskey = true;
        }
        if (other.tag == "SecretRoomKey")
        {
            StartCoroutine(SecretRoomKeyCanvas());
            secretroomdoorlocked.SetActive(false);
            secretroomdooropen.SetActive(true);
            secretroomkey.SetActive(false);
            haskey = true;
        }
        if (other.tag == "DenKey")
        {
            StartCoroutine(DenKeyCanvas());
            dendoorclosed.SetActive(false);
            dendooropen.SetActive(true);
            denkey.SetActive(false);
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
