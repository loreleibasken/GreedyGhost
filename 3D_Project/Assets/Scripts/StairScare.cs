using UnityEngine;
using System.Collections;

public class StairScare : MonoBehaviour {
    public AudioSource spook;
    public AudioClip babyscream;
    public AudioClip bang;
    public GameObject crib;
    public GameObject overturnedcrib;
    public GameObject note;
    public GameObject baby;
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
            baby.SetActive(false);
            overturnedcrib.SetActive(true);
            
          
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
