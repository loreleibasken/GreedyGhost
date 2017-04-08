using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool closed = true;
    public bool inArea = false;
    public GameObject door1;
    public GameObject door2;
    public GameObject ogdoor;
    public GameObject ogdoor2;


    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player")
        {
            inArea = true;
        }
    }



    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(closed == true)
                {
                    ogdoor.SetActive(false);
                    door1.SetActive(true);
                    door2.SetActive(false);
                    closed = false;
                }
                else
                {
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
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
