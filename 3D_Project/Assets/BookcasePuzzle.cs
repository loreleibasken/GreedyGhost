using UnityEngine;
using System.Collections;

public class BookcasePuzzle : MonoBehaviour {

    public GameObject bluebook;
    public GameObject redbook;
    public GameObject greenbook;
    public GameObject redshelf;
    public GameObject blueshelf;
    public GameObject greenshelf;
    public bool hasred;
    public bool hasgreen;
    public bool hasblue;
    public GameObject bookcanvas;


    IEnumerator BookCanvas()
    {
        bookcanvas.SetActive(true);
        yield return new WaitForSeconds(3);
        bookcanvas.SetActive(false);

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(BookCanvas());
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
