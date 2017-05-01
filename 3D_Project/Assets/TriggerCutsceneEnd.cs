using UnityEngine;
using System.Collections;

public class TriggerCutsceneEnd : MonoBehaviour {

    public GameObject door1;
    public GameObject door2;
    public GameObject siren;
    GameObject copAim;
    GameObject copBadge;
    GameObject copPic;
    private Animator AimAnim;
    private Animator BadgeAnim;
    private Animator PicAnim;

    // Use this for initialization
    void Start () {
        copAim = GameObject.Find("policeMan");
        AimAnim = copAim.GetComponent<Animator>();
        copBadge = GameObject.Find("policeChief");
        BadgeAnim = copBadge.GetComponent<Animator>();
        copPic = GameObject.Find("policePhotographer");
        PicAnim = copPic.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something in trigger");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Baby in trigger");
            Instantiate(siren);
            Destroy(door1);
            Destroy(door2);
            AimAnim.SetBool("aimOK", true);
            PicAnim.SetBool("picOk", true);
            BadgeAnim.SetBool("badgeOK", true);
            StartCoroutine(switchScene());
            switchScene();
            
        }
    }

    IEnumerator switchScene()
    {
        yield return new WaitForSeconds(4f);
        Application.LoadLevel("End");
        Destroy(this.gameObject);
    }
}
