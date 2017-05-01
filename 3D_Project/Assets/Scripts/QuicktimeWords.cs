using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class QuicktimeWords : MonoBehaviour {
    //public Text[] uilist;
    public Text UIDisplay;
    public string[] words;
    public int randomLength = 3;
    public float timeout = 3;
    public bool inEvent = false;
    public bool isLethal = true;
    public UnityEvent callOnFail = new UnityEvent();
    public UnityEvent callOnSuccess = new UnityEvent();
    public float failDelay = 0;

    private string prompt;
    private int index;
    private bool dead = false;
	void Start () {
        if(words != null && words.Length > 0)
        { //pick a word
            prompt = words[Random.Range(0, words.Length - 1)].ToUpper();
        } else
        { //generate a random string
            prompt = "";
            for(int i=0;i<randomLength;++i)
            {
                prompt += ((char)(65 + Random.Range(0, 25))).ToString();
            }
        }
        index = 0;
	}
	void Update () {
        if(dead)
        {
            failDelay = Mathf.Max(0, failDelay - Time.deltaTime);
            if(failDelay <= 0)
            {
                if (isLethal) SceneManager.LoadScene("GameOverScreen");
                else
                {
                    UIDisplay.text = "";
                    GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                    GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
                    inEvent = false;
                }
            }
            return;
        }
        if (!inEvent) return;
        timeout -= Time.deltaTime;
        if(timeout <= 0)
        {
            Die();
            return;
        }
        for(int i=0;i<26;++i)
        {
            if(Input.GetKeyDown((KeyCode)(97+i)))
            {
                if((char)(65+i) == prompt[index])
                {
                    //uilist[index].text = "";
                    index++;
                    UIDisplay.text = prompt.Substring(index);
                    if (index >= prompt.Length)
                    {
                        GameObject.Find("Player").GetComponent<Movement>().enabled = true;
                        GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
                        inEvent = false;
                        if (callOnSuccess != null) callOnSuccess.Invoke();
                    }
                } else
                { //pressed the wrong key
                    Die();
                    return;
                }
            }
        }
    }
    void Die()
    {
        dead = true;
        if (callOnFail != null) callOnFail.Invoke();
    }
    void OnTriggerEnter(Collider c)
    {
        StartQuicktimeEvent();
    }
    public bool Untriggered()
    {
        return !inEvent && index < prompt.Length;
    }
    public void StartQuicktimeEvent(bool force = false)
    {
        if(force) { index = 0; dead = false; }
        inEvent = index < prompt.Length; //only true if not happened yet
        if (inEvent)
        {
            //for (int i = 0; i < uilist.Length; ++i)
            //{
            //    if (prompt.Length > i) uilist[i].text = prompt[i].ToString();
            //    else uilist[i].text = "";
            //}
            UIDisplay.text = prompt;
            Vector3 pos = UIDisplay.rectTransform.localPosition;
            pos.x = UIDisplay.minWidth / 2f;
            UIDisplay.rectTransform.localPosition = pos;
        }
    }
}
