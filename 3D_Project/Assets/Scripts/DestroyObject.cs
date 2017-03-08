using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {
    
	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnCollisionEnter()
    {
        GameObject.Destroy(gameObject);
    }
}
