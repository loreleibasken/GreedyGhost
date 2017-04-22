using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    private int spawned;
    public GameObject patron;
    public Transform spawn;


    IEnumerator spawner()
    {
        
        while (true)
        {
            Instantiate(patron, spawn, true);
            yield return new WaitForSeconds(2);



        }


    }





    // Use this for initialization
    void Start () {
        StartCoroutine(spawner());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
