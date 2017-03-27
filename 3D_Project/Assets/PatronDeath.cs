using UnityEngine;
using System.Collections;

public class PatronDeath : MonoBehaviour {



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            GameObject.Find("HUD").GetComponent<GameController>().ded += 1;
            
            Destroy(other.gameObject);
            Application.LoadLevel("GameOverScreen");
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

