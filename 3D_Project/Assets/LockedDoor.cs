using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour {
    public GameObject doorlockedcanvas;



    IEnumerator DoorLocked()
    {
        doorlockedcanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        doorlockedcanvas.SetActive(false);
    }




    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(DoorLocked());
            }
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
