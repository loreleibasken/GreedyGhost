using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class GameController : MonoBehaviour {
    public Text first;
    public Text sec;
    public Text third;
    public int let1 = 0;
    public int let2 = 0;
    public int let3 = 0;
    public char firstletter;
    public char secondletter;
    public char thirdletter;
    public bool inEvent;
    public int v = 0;
    public GameObject self;
    public bool activated = false;
    // Use this for initialization


    IEnumerator countdown()
    {
        GameObject.Find("Player").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOverScreen");
    }




        void OnTriggerEnter()
    {
        inEvent = true;
        //GameObject.Find("Player").GetComponent<Movement>().enabled = false;
        StartCoroutine(countdown());


        let1 = UnityEngine.Random.Range(0, 25);
        do
        {
            let2 = UnityEngine.Random.Range(0, 25);
        } while (let2 == let1);
        do
        {
            let3 = UnityEngine.Random.Range(0, 25);
        } while (let3 == let2 || let3 == let1);

        int ascii = 65;
        for (int i = 0; i < 26; i++)
        {
            ascii++;
            if (let1 == i)
            {
                firstletter = (char)ascii;
            }
            if (let2 == i)
            {
                secondletter = (char)ascii;
            }
            if (let3 == i)
            {
                thirdletter = (char)ascii;
            }
        }



        first.text = firstletter.ToString();
        sec.text = secondletter.ToString();
        third.text = thirdletter.ToString();
    
}




	void Start () {
        
       

        

    }

    // Update is called once per frame
    void Update()
    {
       
        if (first.text == "" && sec.text == "" && third.text == "" && v == 3)
        {
            GameObject.Find("Player").GetComponent<Movement>().enabled = true;
            GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
            inEvent = false;
            StopAllCoroutines();
            activated = true;
        }

        if(activated == true)
        {
            self.SetActive(false);
        }

        if (inEvent == true)
        {
          

            if (v == 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (firstletter == 'A')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (firstletter == 'B')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (firstletter == 'C')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (firstletter == 'D')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (firstletter == 'E')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (firstletter == 'F')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (firstletter == 'G')
                    {
                        first.text = null;
                        v++;
                    }
                    else
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (firstletter == 'H')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'H')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'H')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (firstletter == 'I')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'I')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'I')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (firstletter == 'J')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'J')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'J')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.K))
                {
                    if (firstletter == 'K')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'K')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'K')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (firstletter == 'L')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'L')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'L')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (firstletter == 'M')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'M')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'M')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.N))
                {
                    if (firstletter == 'N')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'N')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'N')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (firstletter == 'O')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'O')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'O')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (firstletter == 'P')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'P')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'P')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (firstletter == 'Q')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'Q')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'Q')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (firstletter == 'R')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'R')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'R')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (firstletter == 'S')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'S')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'S')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (firstletter == 'T')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'T')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'T')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (firstletter == 'U')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'U')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'U')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (firstletter == 'V')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'V')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'V')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (firstletter == 'W')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'W')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'W')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (firstletter == 'X')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'X')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'X')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (firstletter == 'Y')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'Y')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'Y')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (firstletter == 'Z')
                    {
                        first.text = null;
                        v++;
                    }
                    if (secondletter == 'Z')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (thirdletter == 'Z')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
            }
            if( v == 1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (firstletter == 'A')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'A')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'A')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (firstletter == 'B')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'B')
                    {
                        sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'B')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (firstletter == 'C')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'C')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'C')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (firstletter == 'D')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'D')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'D')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (firstletter == 'E')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'E')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'E')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (firstletter == 'F')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'F')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'F')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (firstletter == 'G')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'G')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'G')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (firstletter == 'H')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'H')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'H')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (firstletter == 'I')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'I')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'I')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (firstletter == 'J')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'J')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'J')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.K))
                {
                    if (firstletter == 'K')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'K')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'K')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (firstletter == 'L')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'L')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'L')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (firstletter == 'M')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'M')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'M')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.N))
                {
                    if (firstletter == 'N')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'N')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'N')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (firstletter == 'O')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'O')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'O')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (firstletter == 'P')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'P')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'P')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (firstletter == 'Q')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Q')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'Q')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (firstletter == 'R')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'R')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'R')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (firstletter == 'S')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'S')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'S')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (firstletter == 'T')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'T')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'T')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (firstletter == 'U')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'U')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'U')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (firstletter == 'V')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'V')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'V')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (firstletter == 'W')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'W')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'W')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (firstletter == 'X')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'X')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'X')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (firstletter == 'Y')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Y')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'Y')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }


                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (firstletter == 'Z')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Z')
                    {
                       sec.text = null;
                        v++;
                    }
                    if (thirdletter == 'Z')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
            }
            if(v == 2)
            {

                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (firstletter == 'A')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'A')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'A')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (firstletter == 'B')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'B')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                       
                    }
                    if (thirdletter == 'B')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (firstletter == 'C')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'C')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                       
                    }
                    if (thirdletter == 'C')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (firstletter == 'D')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'D')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'D')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (firstletter == 'E')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'E')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'E')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (firstletter == 'F')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'F')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'F')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (firstletter == 'G')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'G')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'G')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (firstletter == 'H')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'H')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'H')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.I))
                {
                    if (firstletter == 'I')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'I')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'I')
                    {
                        third.text = null;
                        v++;
                    }
                }

                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (firstletter == 'J')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'J')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                       
                    }
                    if (thirdletter == 'J')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.K))
                {
                    if (firstletter == 'K')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'K')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'K')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (firstletter == 'L')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'L')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                       
                    }
                    if (thirdletter == 'L')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (firstletter == 'M')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'M')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'M')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.N))
                {
                    if (firstletter == 'N')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'N')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'N')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.O))
                {
                    if (firstletter == 'O')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'O')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'O')
                    {
                        
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (firstletter == 'P')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'P')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'P')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (firstletter == 'Q')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Q')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                       
                    }
                    if (thirdletter == 'Q')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (firstletter == 'R')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'R')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'R')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (firstletter == 'S')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'S')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'S')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (firstletter == 'T')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'T')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'T')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (firstletter == 'U')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'U')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'U')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (firstletter == 'V')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'V')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'V')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (firstletter == 'W')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'W')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'W')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (firstletter == 'X')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'X')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'X')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.Y))
                {
                    if (firstletter == 'Y')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Y')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'Y')
                    {
                        third.text = null;
                        v++;
                    }
                }


                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (firstletter == 'Z')
                    {
                        SceneManager.LoadScene("GameOverScreen");
                    }
                    if (secondletter == 'Z')
                    {
                         SceneManager.LoadScene("GameOverScreen");
                        
                    }
                    if (thirdletter == 'Z')
                    {
                        third.text = null;
                        v++;
                    }
                }
            }
        }

      

    }
}
