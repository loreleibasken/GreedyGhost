using UnityEngine;
using System.Collections;

public class EventObject : MonoBehaviour {
    public GameObject player = null;
    public bool activated = false;
    public float turnSpeed = 180;
    public float delayTime = 1;
    public AnimatedItem animToRun = null;
    public QuicktimeWords eventToRun = null;
    public bool eventIsReplayable = false;
    public bool animatesTowardPlayer = true;
    //private Movement mscript = null;
    private Vector3 rot;
    private float t = 0;
	void Start() {
        //mscript = player.GetComponent<Movement>();
        rot = transform.localEulerAngles;
	}
	void Update() {
        //if (!activated) return;
        if (t <= 0) return;
        t = Mathf.Max(0, t - Time.deltaTime);
        //they face each other
        Vector3 dir = player.transform.position - eventToRun.gameObject.transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
        rot.y = Mathf.MoveTowardsAngle(rot.y, angle, turnSpeed * Time.deltaTime);
        if(animatesTowardPlayer) eventToRun.gameObject.transform.localEulerAngles = rot;
        Vector3 prot = player.transform.localEulerAngles;
        angle = Mathf.Rad2Deg * Mathf.Atan2(dir.x, -dir.z);
        prot.y = Mathf.MoveTowardsAngle(prot.y, -angle, turnSpeed * Time.deltaTime);
        player.transform.localEulerAngles = prot;
        //quicktime event is triggered after delay
        if(eventToRun.Untriggered() && t <= 0)
        {
            eventToRun.StartQuicktimeEvent(eventIsReplayable);
        }
    }
    public void OnTriggerEnter(Collider c)
    {
        if (activated && !eventIsReplayable) return;
        activated = true;
        t = delayTime;
        GameObject.Find("Player").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
        if(animToRun) animToRun.BeginAction();
    }
}
