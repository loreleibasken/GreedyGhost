using UnityEngine;
using System.Collections;

public class Patron : MonoBehaviour
{
    public GameObject navigator;
    public PatronWaypoint current;
    public PatronWaypoint exit;
    public float waypointDetectRange = 0.5f;

    private NavMeshAgent agent = null;
    private Vector3 pos;

    void Start ()
    {
        if (navigator)
        {
            if (transform.parent != navigator.transform) Debug.Log("Error: Navigator " + navigator.name + " should be the parent object of " + name);
            agent = navigator.GetComponent<NavMeshAgent>();
            if (!agent) Debug.Log("Error: Navigator " + navigator.name + " needs a NavMeshAgent component!");
        }
        GotoNext();
    }
    void GotoNext() {
        current = current.GetNext();
        agent.destination = pos = current.gameObject.transform.position;
    }
	void Update () {
        if ((agent.transform.position - pos).magnitude <= waypointDetectRange) {
            if(current == exit) { //patron survived
               // GameObject.Find("HUD").GetComponent<GameController>().rep += 10;
                Destroy(transform.parent.gameObject);
            } else GotoNext();
        }
    }
}
