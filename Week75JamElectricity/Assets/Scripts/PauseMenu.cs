using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject canvas;
    private bool paused = false;
    private bool canPause = true;

    private void Start() {
        canvas.SetActive(false);
    }


    void Update() {

        if (Input.GetAxisRaw("Cancel") != 0 && canPause) {

            if (paused)
                ResumeGame();
            else
                PauseGame();

            canPause = false;
        }

        if (Input.GetAxisRaw("Cancel") == 0)
            canPause = true;
    }

    public void PauseGame() {
        paused = true;
        canvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame() {
        paused = false;
        canvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitToMainMenu() {
        ResumeGame();
        GameManager.manager.LoadScene("Main Menu", 0.0f);
    }
}
