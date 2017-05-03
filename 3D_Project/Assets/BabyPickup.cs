using UnityEngine;
using System.Collections;

public class BabyPickup : MonoBehaviour {
    public GameObject baby;
    public GameObject babyinarms;
    public GameObject frontdoortrigger;
    public GameObject QTE1;
    public GameObject QTE2;
    public GameObject QTE3;
    public GameObject QTE4;
    public GameObject music;
    public GameObject escapecanvas;


    IEnumerator Escape()
    {
        escapecanvas.SetActive(true);
        yield return new WaitForSeconds(4);
        escapecanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            baby.SetActive(false);
            babyinarms.SetActive(true);
            frontdoortrigger.SetActive(true);
            QTE1.SetActive(true);
            QTE2.SetActive(true);
            QTE3.SetActive(true);
            QTE4.SetActive(true);
            music.SetActive(true);
            StartCoroutine(Escape());
        }
    }
	// Use this for initialization
	void Start () {
        frontdoortrigger.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
