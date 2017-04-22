using UnityEngine;
using System.Collections;

public class LetThereBeLight : MonoBehaviour {

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;
    public GameObject light6;
    public GameObject light7;
    public GameObject light8;
    public GameObject light9;
    public GameObject light10;
    public GameObject light11;
    public GameObject light12;
    public GameObject light13;
    public GameObject flashlight;
    public bool inArea;
    public GameObject spawnbox;
    public GameObject QTE;
    public GameObject sparks;



    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            inArea = true;
        }
    }
    // Use this for initialization
    void Start () {
        inArea = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && inArea == true )
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(true);
            light5.SetActive(true);
            light6.SetActive(true);
            light7.SetActive(true);
            light8.SetActive(true);
            light9.SetActive(true);
            light10.SetActive(true);
            light11.SetActive(true);
            light12.SetActive(true);
            light13.SetActive(true);
            flashlight.SetActive(false);
            spawnbox.SetActive(true);
            QTE.SetActive(true);
            sparks.SetActive(false);
        }
    }
}
