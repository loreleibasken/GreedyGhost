using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool closed = true;
    public bool inArea = false;
    public GameObject door1;
    public GameObject door2;
    public GameObject ogdoor;
    public GameObject ogdoor2;
    public AudioSource door;
    public AudioClip doorsound;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }



    void FixedUpdate()
    {
        if(inArea)
        {
            if(Input.GetKeyDown(KeyCode.E) && (ogdoor.activeSelf || ogdoor2.activeSelf))
            {
                if(closed == true)
                {
                    door.PlayOneShot(doorsound);
                    ogdoor.SetActive(false);
                    door1.SetActive(true);
                    door2.SetActive(false);
                    closed = false;
                }
                else
                {
                    door.PlayOneShot(doorsound);
                    ogdoor2.SetActive(false);
                    door2.SetActive(true);
                    door1.SetActive(false);
                   
                    closed = true;
                   
                }
            }
        }

    }
	// Use this for initialization
	void Start () {
        door1.SetActive(false);
        door2.SetActive(false);
        ogdoor2.SetActive(false);
    }
}
