using UnityEngine;
using System.Collections;

public class MissingBook : MonoBehaviour {
    public GameObject redbook;
    public GameObject bluebook;
    public GameObject greenbook;
    public GameObject redmissing;
    public GameObject bluemissing;
    public GameObject greenmissing;
        
    public GameObject redshelf;
    public GameObject blueshelf;
    public GameObject greenshelf;
    public bool redbookinhand;
    public bool bluebookinhand;
    public bool greenbookinhand;
    public bool redbookdelivered = false;
    public bool bluebookdelievered = false;
    public bool greenbookdelivered = false;
    public GameObject OGpainting;
    public GameObject offsetpainting;
    public GameObject storagekey;
    public int x = 0;
    public bool progressed = false;


    void OnTriggerStay(Collider other)

    {
        if (progressed == true)
        {

            if (other.tag == "BlueBook" && redbookinhand == false && greenbookinhand == false && Input.GetKeyDown(KeyCode.E))
            {
                bluebook.SetActive(true);
                bluemissing.SetActive(false);
                bluebookinhand = true;
            }
            if (other.tag == "RedBook" && bluebookinhand == false && greenbookinhand == false && Input.GetKeyDown(KeyCode.E))
            {
                redbook.SetActive(true);
                redmissing.SetActive(false);
                redbookinhand = true;
            }
            if (other.tag == "GreenBook" && redbookinhand == false && bluebookinhand == false && Input.GetKeyDown(KeyCode.E))
            {
                greenbook.SetActive(true);
                greenmissing.SetActive(false);
                greenbookinhand = true;
            }


            if (other.tag == "Bookshelf")
            {
                if (redbookinhand == true)
                {
                    redshelf.SetActive(true);
                    redbook.SetActive(false);
                    redbookinhand = false;
                    redbookdelivered = true;

                }
                if (bluebookinhand == true)
                {
                    blueshelf.SetActive(true);
                    bluebook.SetActive(false);
                    bluebookinhand = false;
                    bluebookdelievered = true;

                }
                if (greenbookinhand == true)
                {
                    greenshelf.SetActive(true);
                    greenbook.SetActive(false);
                    greenbookinhand = false;
                    greenbookdelivered = true;

                }
            }

        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (redbookdelivered == true && bluebookdelievered == true && greenbookdelivered == true && x == 0)
        {
            OGpainting.SetActive(false);
            offsetpainting.SetActive(true);
            storagekey.SetActive(true);
            x = 1;
        }
	}
}
