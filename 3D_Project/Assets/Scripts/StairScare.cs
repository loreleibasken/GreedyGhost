using UnityEngine;
using System.Collections;

public class StairScare : MonoBehaviour {
    public AudioSource spook;
    public AudioClip babyscream;
    public AudioClip bang;
    public GameObject crib;
    public GameObject overturnedcrib;
    public GameObject note;
    public bool activated = false;
   


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && activated == false)
        {
            activated = true;
            spook.PlayOneShot(babyscream);
            spook.PlayOneShot(bang);
            crib.SetActive(false);
            note.SetActive(true);
            overturnedcrib.SetActive(true);
            GameObject.Find("Player").GetComponent<MissingBook>().progressed = true;
          
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
