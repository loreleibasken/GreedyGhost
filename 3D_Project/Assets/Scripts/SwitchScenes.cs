using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SwitchScenes : MonoBehaviour {
    // attached to each button to switch between scenes
    // note: do not swap Application for SceneManager
	    public void loadGame()
    {
        SceneManager.LoadScene("Lorelei's GameScene");
    }

    public void endGame()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void startGame()
    {
      SceneManager.LoadScene("StartScreen");
    } 

}
