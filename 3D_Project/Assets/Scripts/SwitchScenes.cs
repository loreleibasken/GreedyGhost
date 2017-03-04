using UnityEngine;
using System.Collections;

public class SwitchScenes : MonoBehaviour {
    // attached to each button to switch between scenes
    // note: do not swap Application for SceneManager
	    public void loadGame()
    {
        Application.LoadLevel("Lorelei's GameScene");
    }

    public void endGame()
    {
        Application.LoadLevel("GameOverScreen");
    }

    public void startGame()
    {
        Application.LoadLevel("StartScreen");
    } 
}
