using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Clickable : MonoBehaviour {
    static Clickable currentHighlighted = null;

    public GameObject activeHighlight = null;
    public UnityEvent onLeftClick = new UnityEvent();
    public UnityEvent onRightClick = new UnityEvent();

    private Renderer rend = null;
    private Color defaultColor;
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
            rend.material.color = Color.red;
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
        if (currentHighlighted == this && (activeHighlight == null || activeHighlight.activeSelf))
        {
            if (Input.GetMouseButtonUp(0)) onLeftClick.Invoke();
            else if (Input.GetMouseButtonUp(1)) onRightClick.Invoke();
        }
	}
}
