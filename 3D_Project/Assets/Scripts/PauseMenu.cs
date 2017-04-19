using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public Canvas pausemenu;
    public bool paused = false;

    public void ClickExit()

    {
        Application.Quit();
    }





    void Start()
    {
        pausemenu.enabled = false;
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                paused = true;
                pausemenu.enabled = true;
                Cursor.visible = true;
                GameObject.Find("Player").GetComponent<Movement>().enabled = false;
                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                paused = false;
                Cursor.visible = false;
                pausemenu.enabled = false;
                GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
            }
        }
	}
}
