using UnityEngine;
using System.Collections;

public class KeyInventory : MonoBehaviour {
    GameObject DenKey;

    int den;

	// Use this for initialization
	void Start () {
        DenKey.SetActive(false);
        den = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (den == 1){
            DenKey.SetActive(true);
        }
        if (den ==0){
            DenKey.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "DenKey"){
            den++;
        }
    }

    public void Spend(int a){
        /*Use this method along with another method, to "spend" the key
        use a method like the following to use it

        GameObject player; //make this where the keyinventory script is
        public KeyInventory script;

        script = player.getComponent<KeyInventory>(); //put this into start of other script

        script.Spend(1); //this will remove the key you have in your invetory
        */
        if (a == 1){
            den--;
        }
    }
}
