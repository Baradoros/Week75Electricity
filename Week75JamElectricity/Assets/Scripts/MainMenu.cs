using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {


    public void LoadMainGame() {
        GameManager.manager.LoadScene("Main", 0.0f);
    }

    public void LoadCreditsScene()
    {
        GameManager.manager.LoadScene("Credits", 0.0f);
    }
}
