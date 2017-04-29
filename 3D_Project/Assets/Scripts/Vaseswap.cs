using UnityEngine;
using System.Collections;

public class Vaseswap : MonoBehaviour {

    public GameObject staticvase;
    public GameObject attackvase;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            staticvase.SetActive(false);
            attackvase.SetActive(true);
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
