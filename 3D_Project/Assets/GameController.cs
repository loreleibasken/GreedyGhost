using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    public Text money;
    public Text reputation;
    public Text dead;
    public int cash = 0;
    public int rep = 0;
    public int ded;
	// Use this for initialization
	void Start () {
        
    
	}
	
	// Update is called once per frame
	void Update () {
        cash++;
        money.text = cash.ToString();
        reputation.text = rep.ToString();
        dead.text = ded.ToString();
	}
}
