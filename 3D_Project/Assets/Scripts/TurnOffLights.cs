using UnityEngine;
using System.Collections;

public class TurnOffLights : MonoBehaviour {
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
    public GameObject light14;
    public GameObject flashlight;
    public AudioClip baby;
    public AudioClip mobile;
    public AudioClip thunder;
    public AudioClip click;
    public AudioSource babysounds;
    public bool activated = false;
    public GameObject objtext;
   
    IEnumerator LightDelay()
    {
        babysounds.PlayOneShot(baby);
        babysounds.PlayOneShot(mobile);
        yield return new WaitForSeconds(10);
        babysounds.PlayOneShot(thunder);
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        light4.SetActive(false);
        light5.SetActive(false);
        light6.SetActive(false);
        light7.SetActive(false);
        light8.SetActive(false);
        light9.SetActive(false);
        light10.SetActive(false);
        light11.SetActive(false);
        light12.SetActive(false);
        light13.SetActive(false);
        light14.SetActive(false);
        yield return new WaitForSeconds(3);
        babysounds.PlayOneShot(click);
        flashlight.SetActive(true);
        objtext.SetActive(true);
        yield return new WaitForSeconds(4);
        objtext.SetActive(false);

    } 

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && activated == false)
        {
            activated = true;
            StartCoroutine(LightDelay());
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

