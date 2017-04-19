using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class ShaneQTE : MonoBehaviour
{
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




    void Start()
    {





    }

    // Update is called once per frame
    void Update()
    {

        if (first.text == "" && sec.text == "" && third.text == "")
        {
            GameObject.Find("Player").GetComponent<Movement>().enabled = true;
            GameObject.Find("Player").GetComponent<Rigidbody>().isKinematic = false;
            inEvent = false;
            StopAllCoroutines();
            Debug.Log("won");
            activated = true;
        }

        if (activated == true)
        {
            self.SetActive(false);
        }

        if (inEvent == true)
        {
            int v = 0;

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (firstletter == 'A')
                {
                    first.text = null;
                }
                if (secondletter == 'A')
                {
                    sec.text = null;
                }
                if (thirdletter == 'A')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.B))
            {
                if (firstletter == 'B')
                {
                    first.text = null;
                }
                if (secondletter == 'B')
                {
                    sec.text = null;
                }
                if (thirdletter == 'B')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                if (firstletter == 'C')
                {
                    first.text = null;
                }
                if (secondletter == 'C')
                {
                    sec.text = null;
                }
                if (thirdletter == 'C')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                if (firstletter == 'D')
                {
                    first.text = null;
                }
                if (secondletter == 'D')
                {
                    sec.text = null;
                }
                if (thirdletter == 'D')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (firstletter == 'E')
                {
                    first.text = null;
                }
                if (secondletter == 'E')
                {
                    sec.text = null;
                }
                if (thirdletter == 'E')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.F))
            {
                if (firstletter == 'F')
                {
                    first.text = null;
                }
                if (secondletter == 'F')
                {
                    sec.text = null;
                }
                if (thirdletter == 'F')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.G))
            {
                if (firstletter == 'G')
                {
                    first.text = null;
                }
                if (secondletter == 'G')
                {
                    sec.text = null;
                }
                if (thirdletter == 'G')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.H))
            {
                if (firstletter == 'H')
                {
                    first.text = null;
                }
                if (secondletter == 'H')
                {
                    sec.text = null;
                }
                if (thirdletter == 'H')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.I))
            {
                if (firstletter == 'I')
                {
                    first.text = null;
                }
                if (secondletter == 'I')
                {
                    sec.text = null;
                }
                if (thirdletter == 'I')
                {
                    third.text = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (firstletter == 'J')
                {
                    first.text = null;
                }
                if (secondletter == 'J')
                {
                    sec.text = null;
                }
                if (thirdletter == 'J')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.K))
            {
                if (firstletter == 'K')
                {
                    first.text = null;
                }
                if (secondletter == 'K')
                {
                    sec.text = null;
                }
                if (thirdletter == 'K')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.L))
            {
                if (firstletter == 'L')
                {
                    first.text = null;
                }
                if (secondletter == 'L')
                {
                    sec.text = null;
                }
                if (thirdletter == 'L')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.M))
            {
                if (firstletter == 'M')
                {
                    first.text = null;
                }
                if (secondletter == 'M')
                {
                    sec.text = null;
                }
                if (thirdletter == 'M')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.N))
            {
                if (firstletter == 'N')
                {
                    first.text = null;
                }
                if (secondletter == 'N')
                {
                    sec.text = null;
                }
                if (thirdletter == 'N')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.O))
            {
                if (firstletter == 'O')
                {
                    first.text = null;
                }
                if (secondletter == 'O')
                {
                    sec.text = null;
                }
                if (thirdletter == 'O')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                if (firstletter == 'P')
                {
                    first.text = null;
                }
                if (secondletter == 'P')
                {
                    sec.text = null;
                }
                if (thirdletter == 'P')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (firstletter == 'Q')
                {
                    first.text = null;
                }
                if (secondletter == 'Q')
                {
                    sec.text = null;
                }
                if (thirdletter == 'Q')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                if (firstletter == 'R')
                {
                    first.text = null;
                }
                if (secondletter == 'R')
                {
                    sec.text = null;
                }
                if (thirdletter == 'R')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.S))
            {
                if (firstletter == 'S')
                {
                    first.text = null;
                }
                if (secondletter == 'S')
                {
                    sec.text = null;
                }
                if (thirdletter == 'S')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.T))
            {
                if (firstletter == 'T')
                {
                    first.text = null;
                }
                if (secondletter == 'T')
                {
                    sec.text = null;
                }
                if (thirdletter == 'T')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.U))
            {
                if (firstletter == 'U')
                {
                    first.text = null;
                }
                if (secondletter == 'U')
                {
                    sec.text = null;
                }
                if (thirdletter == 'U')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.V))
            {
                if (firstletter == 'V')
                {
                    first.text = null;
                }
                if (secondletter == 'V')
                {
                    sec.text = null;
                }
                if (thirdletter == 'V')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.W))
            {
                if (firstletter == 'W')
                {
                    first.text = null;
                }
                if (secondletter == 'W')
                {
                    sec.text = null;
                }
                if (thirdletter == 'W')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.X))
            {
                if (firstletter == 'X')
                {
                    first.text = null;
                }
                if (secondletter == 'X')
                {
                    sec.text = null;
                }
                if (thirdletter == 'X')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (firstletter == 'Y')
                {
                    first.text = null;
                }
                if (secondletter == 'Y')
                {
                    sec.text = null;
                }
                if (thirdletter == 'Y')
                {
                    third.text = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (firstletter == 'Z')
                {
                    first.text = null;
                }
                if (secondletter == 'Z')
                {
                    sec.text = null;
                }
                if (thirdletter == 'Z')
                {
                    third.text = null;
                }
            }
        }



    }
}
