using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Clickable : MonoBehaviour {
    static Clickable currentHighlighted = null;

    public GameObject activeHighlight = null;
    public GameObject notecanvas;
    public bool noteup = false;
    public UnityEvent onLeftClick = new UnityEvent();
    public UnityEvent onRightClick = new UnityEvent();
    public bool inArea = false;

    private Renderer rend = null;
    private Color defaultColor;

    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            inArea = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (rend) rend.material.color = defaultColor;
            inArea = false;
        }
    }
    public void Start()
    {
        if (!rend) {
            if (activeHighlight) rend = activeHighlight.GetComponent<Renderer>();
            if (!rend) rend = gameObject.GetComponent<Renderer>();
            if (!rend) rend = gameObject.GetComponentInChildren<Renderer>();
            if (rend) defaultColor = rend.material.color;
            else Debug.Log("Error: " + gameObject.name + " clickable no solid material to highlight, add a child cube.");
        }
    }
    public void OnMouseOver() {
        if (rend) {
           
            currentHighlighted = this;
        } else {
            if (rend) rend.material.color = defaultColor;
        }
    }
    public void OnMouseExit()
    {
        if (rend) rend.material.color = defaultColor;
        if (currentHighlighted == this) currentHighlighted = null;
    }
    void Update() {
        if (inArea == true)
        {
            rend.material.color = Color.gray;
            if (Input.GetMouseButtonUp(0) && noteup == false)
            {
                noteup = true;
                notecanvas.SetActive(true);
                GameObject.Find("Player").GetComponent<Movement>().enabled = false;
                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            if(Input.GetMouseButtonUp(0) && noteup == true)
            {
                noteup = false;
                notecanvas.SetActive(false);
                GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
            }
            //else if (Input.GetMouseButtonUp(1)) onRightClick.Invoke();
        }
	}
}
