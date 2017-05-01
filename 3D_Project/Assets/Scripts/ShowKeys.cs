using UnityEngine;
using System.Collections;

public class ShowKeys : MonoBehaviour {

	void Start () {
	
	}
	
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DenKey")
        {
            gameObject.SetActive(true);
        }
    }
}
