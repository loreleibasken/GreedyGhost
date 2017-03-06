using UnityEngine;
using System.Collections.Generic;

public class PatronWaypoint : MonoBehaviour {
    public List<PatronWaypoint> nearby = new List<PatronWaypoint>();
	void Start () {
	    
	}
    public PatronWaypoint GetNext() {
        if (nearby.Count <= 0) return null;
        return nearby[Random.Range(0, nearby.Count - 1)];
    }
	void Update () {
	    
	}
}
