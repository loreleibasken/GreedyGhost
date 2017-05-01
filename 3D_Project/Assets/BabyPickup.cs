using UnityEngine;
using System.Collections;

public class BabyPickup : MonoBehaviour {
    public GameObject baby;
    public GameObject babyinarms;
    public GameObject frontdoortrigger;



    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            baby.SetActive(false);
            babyinarms.SetActive(true);
            frontdoortrigger.SetActive(true);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
