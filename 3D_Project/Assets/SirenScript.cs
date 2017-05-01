using UnityEngine;
using System.Collections;

public class SirenScript : MonoBehaviour {

    [SerializeField]
    Light redlight;
    [SerializeField]
    Light bluelight;

    private Vector3 redTemp;
    private Vector3 blueTemp;

    [SerializeField]
    int speed;
	
	// Update is called once per frame
	void Update () {
        redTemp.y += speed * Time.deltaTime;
        blueTemp.y -= speed * Time.deltaTime;

        redlight.transform.eulerAngles = redTemp;
        bluelight.transform.eulerAngles = blueTemp;
    }
}
