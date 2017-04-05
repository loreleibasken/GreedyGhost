using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class QuicktimeWords : MonoBehaviour {
    public Text[] uilist;
    public string[] words;
    public int randomLength = 3;
    public float timeout = 3;
    public bool inEvent = false;

    private string prompt;
    private int index;
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
        if (!inEvent) return;
        timeout -= Time.deltaTime;
        if(timeout <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
            return;
        }
        for(int i=0;i<26;++i)
        {
            if(Input.GetKeyDown((KeyCode)(97+i)))
            {
                if((char)(65+i) == prompt[index])
                {
                    uilist[index].text = "";
                    index++;
                    if (index >= prompt.Length)
                    {
                        inEvent = false;
                    }
                } else
                { //pressed the wrong key
                    SceneManager.LoadScene("GameOverScreen");
                    return;
                }
            }
        }
    }
    void OnTriggerEnter(Collider c)
    {
        inEvent = index < prompt.Length; //only true if not happened yet
        if(inEvent)
        {
            for (int i = 0; i < uilist.Length; ++i)
            {
                if (prompt.Length > i) uilist[i].text = prompt[i].ToString();
                else uilist[i].text = "";
            }
        }
    }
}
